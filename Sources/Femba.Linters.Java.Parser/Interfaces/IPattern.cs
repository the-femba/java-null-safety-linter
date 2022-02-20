namespace Femba.Linters.Java.Parser.Interfaces;

/// <summary>
/// Интерфейс декларирующий паттерн поиска определенного объекта.
/// </summary>
/// <typeparam name="TMatcher"></typeparam>
public interface IPattern<TMatcher, TMatching>
{
	/// <summary>
	/// Проверяет, объект соответствует паттерну.
	/// </summary>
	/// <param name="matcher"></param>
	/// <returns></returns>
	public bool IsMatch(TMatcher matcher);
	
	/// <summary>
	/// Преобразует объект в объект соответствующий паттерну.
	/// </summary>
	/// <param name="matcher"></param>
	/// <returns></returns>
	public TMatching Match(TMatcher matcher);
}