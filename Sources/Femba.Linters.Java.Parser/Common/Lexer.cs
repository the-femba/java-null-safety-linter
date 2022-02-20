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
		MaxPosition = Math.Clamp(maxPosition, 0, text.Length);
		MinPosition = Math.Clamp(minPosition, 0, MaxPosition);
		_currentPosition = MinPosition;
		_tokens = new List<Token>();
		_patterns = new HashSet<ILexemePattern>
		{
			new KeywordLexemePattern(),
			new LiteralLexemePattern(),
			new SymbolLexemePattern(),
			new TypeLexemePattern(),
			new NameLexemePattern(),
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
		
		for (var i = CurrentPosition; i < MaxPosition; i++)
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
		for (var i = _currentPosition; i < MaxPosition; i++)
		{
			var symbol = Text[i];
			buffer += symbol;
			var 
		}
	}
}