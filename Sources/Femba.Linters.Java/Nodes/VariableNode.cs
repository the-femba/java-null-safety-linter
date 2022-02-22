using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableNode : Node, IDeclarationNode
{
	public VariableNode(TypeNode type, string name)
	{
		Type = type;
		Name = name;
	}

	public TypeNode Type { get; }
	
	public string Name { get; }
	
	public IReadOnlyList<AnnotationNode> Annotations { get; init; } = new List<AnnotationNode>();

	public override IReadOnlyList<INode> Children => Array.Empty<AnnotationNode>();
}