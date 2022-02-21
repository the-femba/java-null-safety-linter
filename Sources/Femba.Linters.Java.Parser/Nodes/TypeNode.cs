using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class TypeNode : Node, IDeclarationNode
{
	public TypeNode(int startPosition, int endPosition, bool isNullable, string name) : base(startPosition, endPosition)
	{
		IsNullable = isNullable;
		Name = name;
	}
	
	public bool IsNullable { get; }
	
	public string Name { get; }
}