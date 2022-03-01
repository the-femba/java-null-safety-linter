using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Utils;

namespace Femba.Linters.Java.Parser.Patterns.Nodes;

public sealed class FunctionInvokeNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();

		if (!tokens[0].IsName()) return false;
		
		if (tokens.Count < 2) return true;
		
		if (!tokens[1].IsSymbol(TokensNames.ArgumentOpenBasket)) return false;

		if (tokens.Count < 3) return true;
		
		if (tokens[^2].IsSymbol(".") && !tokens[^1].IsName()) return false;

		return !(tokens[^2].IsSymbol(TokensNames.Semicolon));
	}

	public override List<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var tokens = partition.ToList();
		
		var name = tokens[0].Lexeme;

		var values = GetArgumentsTokens(tokens).Select(e => 
			new Common.Parser(e){Patterns = Parser!.Patterns}.ParseNext()!).ToList();

		var func = new FunctionNode(null, name);
		
		var dotSymbolIndex = tokens.FindIndex(e => e.IsSymbol("."));
		
		INode? after = null;
		if (dotSymbolIndex > 0)
		{
			var region = tokens.GetRange(dotSymbolIndex + 1, tokens.Count - dotSymbolIndex - 1);

			after = new Common.Parser(region){Patterns = Parser!.Patterns}.ParseNext();
			
			if (after is not VariableInvokeNode && after is not FunctionInvokeNode) throw new ParseLinterException(
				"After dot must call a field or function.", tokens.First());
		}

		var funcInvoke = new FunctionInvokeNode(func)
		{
			Values = values,
			StartPosition = tokens.First().Position,
			EndPosition = tokens.Last().Position + tokens.Last().Lexeme.Length,
			After = after as IExecutableNode
		};

		func.Parent = funcInvoke;

		foreach (var value in values)
		{
			value.Parent = funcInvoke;
		}
		
		if (after is not null) after.Parent = funcInvoke;

		node = funcInvoke;

		return partition.ToList();
	}
	
	private static List<List<IToken>> GetArgumentsTokens(List<IToken> tokens)
	{
		var closeIndex = tokens.FindIndex(e => e.IsSymbol(TokensNames.ArgumentCloseBasket));

		if (closeIndex == 2) return new List<List<IToken>>();
		tokens = tokens.GetRange(2, closeIndex - 2);
		return tokens.SplitTokens(e => e.IsSymbol(TokensNames.Comma) || e.IsSymbol(TokensNames.ArgumentCloseBasket));
	}
}