using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Utils;

namespace Femba.Linters.Java.Parser.Nodes;

public sealed class LiteralNode : Node
{
	public LiteralNode(string value)
	{
		Value = value;
	}

	public string Value { get; }

	public bool IsNull() => Value == TokensNames.Null;
	
	public bool IsString() => Value[0] == '"';
	
	public bool IsChar() => Value[0] == '\'';
	
	// TODO: Проверять нормально.
	public bool IsNumber() => !IsNull() && !IsString() && !IsChar();
}