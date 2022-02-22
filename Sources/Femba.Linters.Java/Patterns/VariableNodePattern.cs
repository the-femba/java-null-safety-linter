using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class VariableNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		if (partition.Count == 1 && partition[0].IsType()) return true;
		if (partition.Count == 2 && partition[0].IsType() && partition[1].IsName()) return true;
		if (partition.Count == 3 && partition[0].IsType() && partition[1].IsName() &&
		    !(partition[2].IsSymbol() && partition[2].Lexeme == "(")) return true;
		return false;
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var type = partition[0];
		var name = partition[1];

		var typeNode = new TypeNodePattern().Part(new []{type});

		node = new VariableNode((TypeNode) typeNode, name.Lexeme)
		{
			StartPosition = type.Position,
			EndPosition = name.Position + name.Lexeme.Length
		};

		typeNode.Parent = node;

		return new[] {type, name};
	}
}