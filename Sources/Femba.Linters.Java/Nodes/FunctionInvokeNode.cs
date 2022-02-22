using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed record FunctionInvokeNode(FunctionNode Function) : Node, IExecutableNode
{
	public IReadOnlyList<INode> Values { get; init; } = new List<INode>();
	
	public IExecutableNode? After { get; init; }
}