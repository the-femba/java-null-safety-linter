using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Interfaces;

/// <summary>
/// Интерфейс токена. 
/// </summary>
public interface IToken
{
	/// <summary>
	/// Линия, где находится токен в тексте.
	/// </summary>
	int Line { get; }
	
	/// <summary>
	/// Позиция начала токена в линии.
	/// </summary>
	int Position { get; }
	
	// string FileName { get; }
	
	/// <summary>
	/// Тип лексемы.
	/// </summary>
	TokenType Type { get; }
	
	/// <summary>
	/// Значение лексемы.
	/// </summary>
	string Lexeme { get; }
}