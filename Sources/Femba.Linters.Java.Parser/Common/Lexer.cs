using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Lexer : ILexer
{
	private delegate bool ForToEndDelegate(ref int position, char @char);
	
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
		SkipTrash();
		
		var value = "";
		
		var startPosition = _currentPosition;
		ILexemePattern? pattern = null;
		int position;

		for (position = _currentPosition; position <= MaxPosition; position++)
		{
			position += Math.Clamp(SkipTrashAtCurrentPosition() - 1, 0, int.MaxValue);
				
			var @char = Text[position];

			var currentValue = value + @char;
			var currentPattern = _patterns.FirstOrDefault(e => e.IsMatch(currentValue), null);
			
			if (position == MaxPosition)
			{
				if (currentPattern is null) break;
				
				var token = currentPattern.MatchToken(currentValue, startPosition);
				_currentPosition = startPosition + token.Lexeme.Length;

				return token;
			}
			
			if (currentPattern is null)
			{
				if (pattern is null) break;
				
				var token = pattern.MatchToken(value, startPosition);
				_currentPosition = startPosition + token.Lexeme.Length;

				return token;
			}

			pattern = currentPattern;
			value = currentValue;
			_currentPosition = position;
		}

		_currentPosition = startPosition;
		return null;
	}

	private int SkipTrashAtCurrentPosition()
	{
		var @char = Text[_currentPosition];

		if (IsUnSupportedSymbol(@char) && @char is not ' ')
		{
			_currentPosition += 1;
			return 1;
		}

		if (_currentPosition < MaxPosition && IsComment(@char, Text[_currentPosition + 1]))
		{
			return SkipComment();
		}

		return 0;
	}

	private int SkipTrash() =>
		ForToEnd((ref int position, char @char) =>
		{
			if (position < MaxPosition && IsComment(@char, Text[position + 1]))
			{
				position += SkipComment();
				return true;
			}
			
			return IsUnSupportedSymbol(@char);
		});

	private int SkipComment() =>
		Text[_currentPosition + 1] is '/' 
			? SkipLineComment() 
			: SkipClosedComment();

	private int SkipLineComment() =>
		ForToEnd((ref int _, char @char) => @char is '\n' || true);
	
	private int SkipClosedComment() =>
		ForToEnd((ref int position, char @char) => position >= MaxPosition || @char is not '*' || Text[position + 1] is not '/');
	
	private int ForToEnd(ForToEndDelegate func)
	{
		var startIndex = _currentPosition;
		int index;
		
		for (index = _currentPosition; index <= MaxPosition; index++)
		{
			if (!func(ref index, Text[index])) break;
		}

		_currentPosition = index;
		
		return index - startIndex;
	}

	private bool IsUnSupportedSymbol(char @char) => @char is ' ' or '\n' or '/';
	
	private bool IsComment(char @char, char @nextChar) => @char is '/' || @nextChar is '/' or '*';
}