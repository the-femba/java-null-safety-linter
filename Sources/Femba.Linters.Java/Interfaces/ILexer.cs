namespace Femba.Linters.Java.Parser.Interfaces;

/// <summary>
/// Интерфейс лексера.
/// </summary>
public interface ILexer
{
	IReadOnlyList<ITokenPattern> Patterns { get; }

	/// <summary>
	/// Исходный текст.
	/// </summary>
	string Text { get; }
	
	/// <summary>
	/// Список всех лексированных токенов.
	/// </summary>
	IReadOnlyList<IToken> Tokens { get; }
	
	/// <summary>
	/// Пытается получить следующий токен. Если токен не найден, то возвращает null.
	/// </summary>
	/// <returns>Токен.</returns>
	IToken? LexNext();
	
	/// <summary>
	/// Пытается получить следующий токен до конца, пока не получит null.
	/// </summary>
	/// <returns>Токены, которые были получены за время вызова этого метода.</returns>
	List<IToken> LexToEnd();
}