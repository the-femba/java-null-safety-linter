using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Lexer : ILexer
{
	private HashSet<ILexemePattern> _patterns;
	
	private List<Token> _tokens;

	private int _currentPosition;
	
	public Lexer(string text, int minPosition = 0, int maxPosition = int.MaxValue, int positionOffset = 0, List<ILexemePattern>? patterns = null)
	{
		Text = text;
		PositionOffset = positionOffset;
		MaxPosition = Math.Clamp(maxPosition, 0, text.Length - 1);
		MinPosition = Math.Clamp(minPosition, 0, MaxPosition);
		_currentPosition = MinPosition;
		_tokens = new List<Token>();
		_patterns = new HashSet<ILexemePattern>
		{
			new NamePattern(),
			new LiteralPattern(),
			new KeywordPattern(),
			new SymbolPattern(),
			new TypePattern()
		};
		if (patterns is not null) foreach (var pattern in patterns) _patterns.Add(pattern);
	}
	
	public string Text { get; }

	public IReadOnlyList<IToken> Tokens => _tokens;

	public int CurrentPosition => _currentPosition;

	public int MinPosition { get; }

	public int MaxPosition { get; }

	public int PositionOffset { get; }

	public int OffsetedCurrentPosition => PositionOffset + CurrentPosition;

	public int OffsetedMinPosition => PositionOffset + MinPosition;

	public int OffsetedMaxPosition => PositionOffset + MaxPosition;
	
	public IList<IToken> NextToEnd()
	{
		var list = new List<IToken>();
		
		for (var i = CurrentPosition; i <= MaxPosition; i++)
		{
			var token = Next();
			if (token is null) break;
			list.Add(token);
		}

		return list;
	}

	public IToken? Next()
	{
		var buffer = "";
		List<ILexemePattern>? patterns = null;

		for (var i = _currentPosition; i <= MaxPosition; i++)
		{
			var symbol = Text[i];

			var currentBuffer = buffer;
			if (symbol == ' ' && buffer == "") _currentPosition++;
			else currentBuffer += symbol;

			var currentPatterns = _patterns
				.Where(e => e.IsMatch(currentBuffer))
				.ToList();

			if (currentBuffer != "" && currentPatterns.Count == 0)
			{
				var pattern = patterns?.FirstOrDefault();

				if (pattern is null) return null;

				var result = pattern.MatchToken(buffer, _currentPosition);

				_currentPosition += result.Lexeme.Length;

				return result;
			}

			patterns = currentPatterns;
			buffer = currentBuffer;
		}

		return null;
	}
}