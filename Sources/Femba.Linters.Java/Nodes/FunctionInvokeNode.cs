using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class FunctionInvokeNode : Node, IExecutableNode
{
	public FunctionInvokeNode(FunctionNode function, IExecutableNode? after)
	{
		Function = function;
		After = after;
	}

	public FunctionNode Function { get; }
	
	public IReadOnlyList<INode> Values { get; init; } = Array.Empty<INode>();
	
	public IExecutableNode? After { get; init; }
}