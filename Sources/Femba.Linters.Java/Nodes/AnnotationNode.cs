using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class AnnotationNode : Node, IDeclarationNode
{
	public AnnotationNode(TypeNode type)
	{
		Type = type;
	}

	public TypeNode Type { get; }

	public override IReadOnlyList<INode> Children => Array.Empty<INode>();
}