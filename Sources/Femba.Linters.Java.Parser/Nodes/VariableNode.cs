using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableNode : Node, IDeclarationNode
{
	public VariableNode(int startPosition, int endPosition, TypeNode type, string name,
		List<AnnotationNode>? annotations = null) : base(startPosition, endPosition)
	{
		Name = name;
		Annotations = annotations ?? new List<AnnotationNode>();
		Type = type;
	}

	public IReadOnlyList<AnnotationNode> Annotations { get; }
	
	public TypeNode Type { get; }
	
	public string Name { get; }
}