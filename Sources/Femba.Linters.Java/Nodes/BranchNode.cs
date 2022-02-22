using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class BranchNode : Node, IExecutableNode
{
	public BranchNode(ConditionNode condition)
	{
		Condition = condition;
	}

	public ConditionNode Condition { get; }

	public IReadOnlyList<INode> Stick { get; init; } = Array.Empty<INode>();
	
	public override IReadOnlyList<INode> Children => Stick;
}