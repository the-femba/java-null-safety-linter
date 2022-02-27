using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public class LiteralNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		return partition.Count == 1 && partition[0].IsLiteral();
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var literal = partition[0];
		
		node = new LiteralNode(literal.Lexeme)
		{
			StartPosition = literal.Position,
			EndPosition = literal.Position + literal.Lexeme.Length
		};
		
		return partition.Take(1).ToList();
	}
}