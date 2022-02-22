using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed record FunctionNode(TypeNode Type, string Name) : Node, IDeclarationNode
{
	public IReadOnlyList<AnnotationNode> Annotations { get; init; } = new List<AnnotationNode>();

	public IReadOnlyList<VariableNode> Arguments { get; init; } = new List<VariableNode>();

	public IReadOnlyList<INode> Body { get; init; } = new List<INode>();
}