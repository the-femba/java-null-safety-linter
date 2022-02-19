namespace Femba.Linters.Java.Parser.Interfaces;

/// <summary>
/// Интерфейс лексера.
/// </summary>
public interface ILexer
{
	/// <summary>
	/// Исходный текст.
	/// </summary>
	string Text { get; }
	
	/// <summary>
	/// Список всех лексированных токенов.
	/// </summary>
	IReadOnlyList<IToken> Tokens { get; }

	/// <summary>
	/// Текущая лексическая позиция в тексте.
	/// </summary>
	int CurrentPosition { get; }
	
	/// <summary>
	/// Минимальная возможная позиция в тексте.
	/// </summary>
	int MinPosition { get; }
	
	/// <summary>
	/// Максимально возможная позиция в тексте.
	/// </summary>
	int MaxPosition { get; }
	
	/// <summary>
	/// Искусственный отступ позиции. Например парсится файла и нужно указать искусственный отступ.
	/// </summary>
	int PositionOffset { get; }
	
	/// <summary>
	/// Искусственная текущая лексическая позиция в тексте с учетом <see cref="PositionOffset"/>.
	/// </summary>
	int OffsetedCurrentPosition { get; }
	
	/// <summary>
	/// Искусственная минимальная возможная лексическая позиция в тексте с учетом <see cref="PositionOffset"/>.
	/// </summary>
	int OffsetedMinPosition { get; }
	
	/// <summary>
	/// Искусственная максимальная возможная лексическая позиция в тексте с учетом <see cref="PositionOffset"/>.
	/// </summary>
	int OffsetedMaxPosition { get; }
	
	/// <summary>
	/// Пытается получить следующий токен. Если токен не найден, то возвращает null.
	/// </summary>
	/// <returns>Токен.</returns>
	IToken? Next();
	
	/// <summary>
	/// Пытается получить следующий токен до конца, пока не получит null.
	/// </summary>
	/// <returns>Токены, которые были получены за время вызова этого метода.</returns>
	IList<IToken> NextToEnd();
}