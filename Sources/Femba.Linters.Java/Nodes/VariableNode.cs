using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed record VariableNode(TypeNode Type, string Name) : Node, IDeclarationNode
{
	public IReadOnlyList<AnnotationNode> Annotations { get; init; } = new List<AnnotationNode>();
}