using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class FunctionNodePattern : NodePattern<FunctionNode>
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		return partition.Count switch
		{
			1 => partition.First().IsType(),
			2 => partition[0].IsType() && partition[1].IsName(),
			> 2 => partition[0].IsType() && partition[1].IsName() && IsWithTypes(partition),
			_ => false
		};
	}

	private bool IsWithTypes(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();
		tokens.RemoveRange(0, 2);
		
		if (tokens.Count == 1 && tokens[0].IsSymbol() && tokens[0].Lexeme == "(") return true;
		tokens.RemoveAt(0);

		var variablePattern = new VariableNodePattern();
		
		IToken? lastToken = null;
		var isLastBasket = false;
		int i;
		for (i = 0; i < tokens.Count; i++)
		{
			if (i != 0 && i % 2 == 0)
			{
				lastToken = null;
				continue;
			}
			
			var currentToken = tokens[i];

			if (currentToken.IsSymbol(")"))
			{
				if (tokens.Count < 3 && tokens.Count - 1 % 2 != 0) return false;
				var preToken = tokens[i - 1];
				var prePreToken = tokens[i - 2];
				if (!variablePattern.IsPart(new[] {prePreToken, preToken})) return false;
				isLastBasket = true;
			}

			if (lastToken is null)
			{
				if (!variablePattern.IsPart(new[] {currentToken})) return false;
			}
			else
			{
				if (!variablePattern.IsPart(new[] {lastToken, currentToken})) return false;
			}

			lastToken = currentToken;
		}

		if (!isLastBasket) return true;
		
		tokens.RemoveRange(0, i);
		if (tokens.Count == 0) return true;
		if (tokens.First().IsSymbol("{")) return true;
		if (tokens.First().IsSymbol("{") && tokens.Last().IsSymbol("}")) return true;
		return false;
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out FunctionNode node)
	{
		if (!partition.Last().IsSymbol("}")) throw new ParseLinterException(
			"The function format is bad.", partition.First());

		var type = new TypeNodePattern().Part(new []{partition[0]});
		var name = partition[1].Lexeme;

		var arguments = new List<VariableNode>();
		
		if (!partition[4].IsSymbol(")"))
		{
			for (int i = 5; i < partition.Count; i += 2)
			{
				var first = partition[i];
				var second = partition[i - 1];

				var arg = new VariableNodePattern().Part(new []{first, second});
				
				arguments.Add(arg);
			}
		}

		var openBodyBasketIndex = partition.ToList().FindIndex(e => e.IsSymbol("{"));
		var bodyTokens = partition.ToList().GetRange(openBodyBasketIndex + 1, partition.Count - 2);

		var body = new Common.Parser(bodyTokens).ParseToEnd();

		var func = new FunctionNode(type, name)
		{
			Arguments = arguments,
			Body = body.ToList()
		};
		
		foreach (var bodyNode in body)
		{
			bodyNode.Parent = func;
		}

		foreach (var argument in arguments)
		{
			argument.Parent = func;
		}

		node = func;

		return partition;
	}
}