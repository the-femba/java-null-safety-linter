namespace Femba.Linters.Java.Parser.Enums;

/// <summary>
/// Тип токена.
/// </summary>
public enum TokenType
{
	Unknown = -1,
	
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
	Literal,
	
	/// <summary>
	/// Ключевые слова, например: if, else, switch, return, break, this, new.
	/// Void, string и другие ключевые слова, которые являются типами, относятся к типам.
	/// </summary>
	Keyword,
}