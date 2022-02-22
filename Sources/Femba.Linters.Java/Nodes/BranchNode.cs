using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed record BranchNode : Node, IExecutableNode
{
	public IReadOnlyDictionary<ConditionNode, IReadOnlyList<INode>> Sticks { get; init; } =
		new Dictionary<ConditionNode, IReadOnlyList<INode>>();

	public IReadOnlyList<ConditionNode> Conditions => Sticks.Keys.ToList();
}