using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class TypeNode : Node, IDeclarationNode
{
	public TypeNode(string name)
	{
		Name = name;
	}

	public bool IsNullable { get; init; }

	public string Name { get; }

	public override IReadOnlyList<INode> Children =>  Array.Empty<INode>();
}