using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Lexer : ILexer
{
	private delegate bool ForToEndDelegate(ref int position, char @char);
	
	private HashSet<ILexemePattern> _patterns;
	
	private List<IToken> _tokens;

	private int _currentPosition;
	
	public Lexer(string text, int minPosition = 0, int maxPosition = int.MaxValue, int positionOffset = 0, List<ILexemePattern>? patterns = null)
	{
		Text = text;
		PositionOffset = positionOffset;
		MaxPosition = Math.Clamp(maxPosition, 0, text.Length - 1);
		MinPosition = Math.Clamp(minPosition, 0, MaxPosition);
		_currentPosition = MinPosition;
		_tokens = new List<IToken>();
		// Патерны в порядке приоритета важности растравляются. Не забыть.
		_patterns = new HashSet<ILexemePattern>
		{
			new NumberLiteralPattern(),
			new StringLiteralPattern(),
			new CharLiteralPattern(),
			new KeywordPattern(),
			new NamePattern(),
			new SymbolPattern(),
			new TypePattern()
		};
		if (patterns is null) return;
		foreach (var pattern in patterns) _patterns.Add(pattern);
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
	
	public IList<IToken> LexToEnd()
	{
		var list = new List<IToken>();
		
		for (int index = CurrentPosition; index <= MaxPosition; index++)
		{
			var token = LexNext();
			if (token is null) break;
			list.Add(token);
		}

		return list;
	}

	public IToken? LexNext()
	{
		var value = "";
		
		ILexemePattern? pattern = null;
		int position;

		for (position = _currentPosition; position <= MaxPosition; position++)
		{
			var @char = Text[position];

			var currentValue = value + @char;
			var formattedValue = currentValue.Trim();
			if (formattedValue == "")
			{
				value = currentValue;
				continue;
			}
			var currentPattern = _patterns.FirstOrDefault(e => e != null && e.IsMatch(currentValue.Trim()), null);
			
			if (currentPattern is null)
			{
				if (pattern is null) break;
				
				var token = pattern.MatchToken(value.Trim(), _currentPosition);
				_currentPosition += token.Lexeme.Length + (value.Length - value.Trim().Length);
				
				_tokens.Add(token);
				return token;
			}

			pattern = currentPattern;
			value = currentValue;
		}
		
		if (pattern is null) return null;
				
		var token2 = pattern.MatchToken(value.Trim(), _currentPosition);
		_currentPosition += token2.Lexeme.Length + (value.Length - value.Trim().Length);
				
		_tokens.Add(token2);
		return token2;
	}

	private int SkipTrashAtCurrentPosition(int? fromPosition = null)
	{
		var startPosition = fromPosition ?? _currentPosition;
		var @char = Text[startPosition];

		if (IsUnSupportedSymbol(@char) && @char is not ' ')
		{
			return 1;
		}

		if (startPosition < MaxPosition && IsComment(@char, Text[startPosition + 1]))
		{
			return SkipComment(startPosition);
		}

		return 0;
	}

	private int SkipTrash(int? fromPosition = null) =>
		ForToEnd((ref int position, char @char) =>
		{
			if (position < MaxPosition && IsComment(@char, Text[position + 1]))
			{
				position += SkipComment(position);
				return true;
			}
			
			return IsUnSupportedSymbol(@char);
		}, fromPosition);

	private int SkipComment(int? fromPosition = null) =>
		Text[(fromPosition ?? _currentPosition) + 1] is '/' 
			? SkipLineComment(fromPosition) 
			: SkipClosedComment(fromPosition);

	private int SkipLineComment(int? fromPosition = null) =>
		ForToEnd((ref int _, char @char) => @char is '\n' || true, fromPosition);
	
	private int SkipClosedComment(int? fromPosition = null) =>
		ForToEnd((ref int position, char @char) =>
			position >= MaxPosition || @char is not '*' || Text[position + 1] is not '/', fromPosition);
	
	private int ForToEnd(ForToEndDelegate func, int? fromPosition = null)
	{
		var startIndex = fromPosition ?? _currentPosition;
		int index;
		
		for (index = startIndex; index <= MaxPosition; index++)
		{
			if (!func(ref index, Text[index])) break;
		}
		
		return index - startIndex;
	}

	private bool IsUnSupportedSymbol(char @char) => @char is ' ' or '\n' or '/';
	
	private bool IsComment(char @char, char @nextChar) => @char is '/' || @nextChar is '/' or '*';
}