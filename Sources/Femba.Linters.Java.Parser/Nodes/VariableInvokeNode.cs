using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class VariableInvokeNode : Node, IExecutableNode
{
	public VariableInvokeNode(int startPosition, int endPosition, VariableNode variable, IExecutableNode? after) : base(startPosition, endPosition)
	{
		Variable = variable;
		After = after;
	}
	
	public VariableNode Variable;
	
	public IExecutableNode? After { get; }
}