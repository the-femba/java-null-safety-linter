using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class ConditionNode : Node, IDeclarationNode
{
	public override IReadOnlyList<INode> Children => Array.Empty<INode>();
}