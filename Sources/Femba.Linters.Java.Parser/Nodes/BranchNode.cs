using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class BranchNode : Node, IExecutableNode
{
	public BranchNode(int startPosition, int endPosition,
		Dictionary<ConditionNode, IReadOnlyList<INode>> sticks) : base(startPosition, endPosition)
	{
		Sticks = sticks;
	}

	public IReadOnlyDictionary<ConditionNode, IReadOnlyList<INode>> Sticks { get; }

	public IReadOnlyList<ConditionNode> Conditions => Sticks.Keys.ToList();
}