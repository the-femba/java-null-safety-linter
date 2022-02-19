namespace Femba.Linters.Java.Parser.Enums;

/// <summary>
/// Тип токена.
/// </summary>
public enum TokenType
{
	/// <summary>
	/// Типы, например: string, text, Point.
	/// </summary>
	Type,
	/// <summary>
	/// Имена функций и переменных, например: myVar1, myFunc1.
	/// </summary>
	Name,
	/// <summary>
	/// Символы языка, например: @, {, }, (, ), +, -, /, *, =, >, ;. И множество других.
	/// </summary>
	Symbol,
	/// <summary>
	/// Константы, например: "text", 23.45, 23.
	/// </summary>
	Literal
}