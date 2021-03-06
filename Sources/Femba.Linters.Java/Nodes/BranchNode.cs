using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class BranchNode : Node, IExecutableNode
{
	public BranchNode(ConditionNode? condition)
	{
		Condition = condition;
	}

	public ConditionNode? Condition { get; }

	public IReadOnlyList<INode> Body { get; init; } = new List<INode>();

	public bool IsEnd { get; init;  } = false;
}