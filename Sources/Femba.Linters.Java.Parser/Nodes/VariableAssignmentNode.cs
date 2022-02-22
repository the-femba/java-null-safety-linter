using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableAssignmentNode : Node, IExecutableNode
{
	public VariableAssignmentNode(int startPosition, int endPosition, VariableNode variable,
		INode assignment) : base(startPosition, endPosition)
	{
		Variable = variable;
		Assignment = assignment;
	}
	
	public VariableNode Variable { get; }
	
	public INode Assignment { get; }
}