namespace Femba.Linters.Java.Parser.Interfaces;

// TODO: Сделать работу с патерном лучше.
/// <summary>
/// Интерфейс декларирующий паттерн поиска определенного объекта.
/// </summary>
public interface IPattern
{
	/// <summary>
	/// Проверяет, объект соответствует паттерну.
	/// </summary>
	/// <param name="matcher"></param>
	/// <returns></returns>
	public bool IsMatch(string matcher);
	
	/// <summary>
	/// Преобразует объект в объект соответствующий паттерну.
	/// </summary>
	/// <param name="matcher"></param>
	/// <returns></returns>
	public string MatchLexeme(string matcher);

	/// <summary>
	/// Преобразует объект в объект соответствующий паттерну.
	/// </summary>
	/// <param name="matcher"></param>
	/// <param name="position"></param>
	/// <returns></returns>
	public IToken MatchToken(string matcher, int position);
}