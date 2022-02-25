using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class FunctionInvokeNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();

		if (!tokens[0].IsName()) return false;
		
		if (tokens.Count < 2) return true;
		
		if (!tokens[1].IsSymbol("(")) return false;

		if (tokens.Count < 3) return true;
		
		if (tokens[^2].IsSymbol(".") && !tokens[^1].IsName()) return false;

		return !tokens[^2].IsSymbol(";");
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		throw new NotImplementedException();
	}
}