using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableAssignmentNode : Node, IExecutableNode
{
	public VariableAssignmentNode(VariableNode variable, INode assignment)
	{
		Variable = variable;
		Assignment = assignment;
	}

	public VariableNode Variable { get; }

	public INode Assignment { get; }

	public override IReadOnlyList<INode> Children => Array.Empty<INode>();
}