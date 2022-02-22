using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class AnnotationNodePattern : NodePattern<AnnotationNode>
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		if (partition.Count == 1 && IsDogSymbol(partition[0])) return true;
		if (partition.Count == 2 && IsDogSymbol(partition[0]) && partition[1].IsType()) return true;
		return false;
	}

	private bool IsDogSymbol(IToken token) => token.IsSymbol() && token.Lexeme == "@";

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out AnnotationNode node)
	{
		var type = partition[1];

		var typeNode = new TypeNodePattern().Part(new []{type});

		node = new AnnotationNode(typeNode);
		
		typeNode.Parent = node;
		
		return partition.Take(2).ToArray();
	}
}