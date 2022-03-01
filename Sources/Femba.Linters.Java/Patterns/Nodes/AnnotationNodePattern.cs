using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Utils;

namespace Femba.Linters.Java.Parser.Patterns.Nodes;

public sealed class AnnotationNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		if (partition.Count == 1 && IsDogSymbol(partition[0])) return true;
		if (partition.Count == 2 && IsDogSymbol(partition[0]) && partition[1].IsType()) return true;
		return false;
	}

	private bool IsDogSymbol(IToken token) => token.IsSymbol() && token.Lexeme == TokensNames.Dog;

	public override List<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var type = partition[1];

		var typeNode = new TypeNodePattern().Part(new []{type});
		
		node = UpdateParentsUtils.UpdateAnnotation(new AnnotationNode((TypeNode) typeNode));
		return partition.Take(2).ToList();
	}
}