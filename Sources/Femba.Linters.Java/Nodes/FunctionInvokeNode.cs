using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class FunctionInvokeNode : Node, IExecutableNode
{
	public FunctionInvokeNode(int startPosition, int endPosition, FunctionNode function,
		List<INode>? values = null, IExecutableNode? after = null) : base(startPosition, endPosition)
	{
		Function = function;
		Values = values ?? new List<INode>();
		After = after;
	}

	public FunctionNode Function;

	public IReadOnlyList<INode> Values;
	
	public IExecutableNode? After { get; }
}