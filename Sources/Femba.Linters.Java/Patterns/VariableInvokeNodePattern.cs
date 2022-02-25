using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class VariableInvokeNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();

		if (!tokens[0].IsName()) return false;
		
		if (tokens.Count < 2) return true;
		
		if (!tokens[1].IsSymbol(".")) return false;

		if (tokens.Count < 3) return true;
		
		if (tokens[^2].IsSymbol(".") && !tokens[^1].IsName()) return false;

		return !(tokens[^2].IsSymbol(";"));
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var tokens = partition.ToList();

		var name = partition[0].Lexeme;

		if (tokens.Last().IsSymbol(";")) tokens.RemoveAt(tokens.Count - 1);
		
		var dotSymbolIndex = tokens.FindIndex(e => e.IsSymbol("."));

		INode? after = null;
		if (dotSymbolIndex > 0)
		{
			var region = tokens.GetRange(dotSymbolIndex + 1, tokens.Count - dotSymbolIndex - 1);

			after = new Common.Parser(region, Parser!.Patterns.ToList()).ParseNext();
			
			if (after is not VariableInvokeNode && after is not FunctionInvokeNode) throw new ParseLinterException(
				"After dot must call a field or function.", tokens.First());
		}

		var varInvoke = new VariableInvokeNode(new VariableNode(null, name))
		{
			After = after as IExecutableNode,
			StartPosition = tokens.First().Position,
			EndPosition = dotSymbolIndex > 0 
				? tokens.Last().Position + tokens.Last().Lexeme.Length 
				: tokens.First().Position + tokens.First().Lexeme.Length,
		};

		if (after is not null) after.Parent = varInvoke;

		node = varInvoke;
		
		return partition;
	}
}