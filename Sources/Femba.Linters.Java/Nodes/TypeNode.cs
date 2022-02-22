using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed record TypeNode(string Name) : Node, IDeclarationNode
{
	public bool IsNullable { get; init; }
}