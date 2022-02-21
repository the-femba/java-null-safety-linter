using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class FunctionNode : Node, IDeclarationNode
{
	public FunctionNode(int startPosition, int endPosition, TypeNode type, string name,
		List<VariableNode>? arguments = null, List<INode>? body = null,
		List<AnnotationNode>? annotations = null) : base(startPosition, endPosition)
	{
		Name = name;
		Annotations = annotations ?? new List<AnnotationNode>();
		Type = type;
		Arguments = arguments ?? new List<VariableNode>();
		Body = body ?? new List<INode>();
	}
	
	public IReadOnlyList<AnnotationNode> Annotations { get; }

	public string Name { get; }
	
	public TypeNode Type { get; }
	
	public IReadOnlyList<VariableNode> Arguments { get; }
	
	public IReadOnlyList<INode> Body { get; }
}