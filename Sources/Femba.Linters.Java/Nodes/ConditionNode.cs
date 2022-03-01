using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class ConditionNode : Node, IDeclarationNode
{
	public IReadOnlyList<IToken> Values { get; init; } = new List<IToken>();
}