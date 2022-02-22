using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableInvokeNode : Node, IExecutableNode
{
	public VariableInvokeNode(VariableNode variable)
	{
		Variable = variable;
	}

	public VariableNode Variable { get; }
	
	public IExecutableNode? After { get; init; }

	public override IReadOnlyList<INode> Children =>  Array.Empty<INode>();
}