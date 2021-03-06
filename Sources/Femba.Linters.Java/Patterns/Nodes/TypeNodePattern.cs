using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns.Nodes;

public sealed class TypeNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		return partition.Count == 1 && partition.First().IsType();
	}

	public override List<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var first = partition.First();
		
		node = new TypeNode(first.Lexeme)
		{
			StartPosition = first.Position,
			EndPosition = first.Position + first.Lexeme.Length
		};
		
		return new List<IToken>{first};
	}
}