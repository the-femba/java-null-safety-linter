namespace Femba.Linters.Java.Parser.Interfaces;

// TODO: Сделать работу с патерном лучше.
/// <summary>
/// Интерфейс декларирующий паттерн поиска определенного объекта.
/// </summary>
public interface ITokenPattern : IPattern<string, IToken>
{
	IToken Part(string matcher, int position);
}