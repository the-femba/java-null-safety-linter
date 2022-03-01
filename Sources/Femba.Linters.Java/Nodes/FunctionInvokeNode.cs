using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class FunctionInvokeNode : Node, IExecutableNode
{
	public FunctionInvokeNode(FunctionNode function)
	{
		Function = function;
	}

	public FunctionNode Function { get; }
	
	public IReadOnlyList<INode> Values { get; init; } = new List<INode>();
	
	public IExecutableNode? After { get; init; }
}