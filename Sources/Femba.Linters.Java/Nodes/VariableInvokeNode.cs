using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed record VariableInvokeNode(VariableNode Variable) : Node, IExecutableNode
{
	public IExecutableNode? After { get; init; }
}