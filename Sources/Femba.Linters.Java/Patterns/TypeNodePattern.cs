using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class TypeNodePattern : NodePattern<TypeNode>
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		return partition.Count == 1 && partition.First().IsType();
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out TypeNode node)
	{
		var first = partition.First();
		
		node = new TypeNode(first.Lexeme)
		{
			StartPosition = first.Position,
			EndPosition = first.Position + first.Lexeme.Length
		};
		
		return new []{first};
	}
}