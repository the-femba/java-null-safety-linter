using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public class ConditionNode : Node, IDeclarationNode
{
	public ConditionNode(int startPosition, int endPosition) : base(startPosition, endPosition)
	{
		
	}
}