using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class LiteralNode : Node
{
	public LiteralNode(string value)
	{
		Value = value;
	}

	public string Value { get; }

	public bool IsNull() => Value == "null";
	
	public bool IsString() => Value[0] == '"';
	
	public bool IsChar() => Value[0] == '\'';
	
	public bool IsNumber() => !IsNull() && !IsString() && !IsChar();
}