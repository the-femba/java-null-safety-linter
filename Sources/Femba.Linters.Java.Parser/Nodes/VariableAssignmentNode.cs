using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableAssignmentNode : Node, IExecutableNode
{
	public VariableAssignmentNode(int startPosition, int endPosition, VariableNode variable,
		List<INode> assignments) : base(startPosition, endPosition)
	{
		Variable = variable;
		Assignments = assignments;
	}
	
	public VariableNode Variable { get; }
	
	public IReadOnlyList<INode> Assignments { get; }
}