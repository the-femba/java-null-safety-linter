using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class AnnotationNode : Node, IDeclarationNode
{
	public AnnotationNode(int startPosition, int endPosition, TypeNode type) : base(startPosition, endPosition)
	{
		Type = type;
	}
	
	public TypeNode Type { get; }
}