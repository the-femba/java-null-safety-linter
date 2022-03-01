using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Lexer : ILexer
{
	private delegate bool ForToEndDelegate(ref int position, char @char);
	
	private readonly List<IToken> _tokens = new List<IToken>();

	private int _currentPosition = 0;
	
	public Lexer(string text)
	{
		Text = text;
	}

	public IReadOnlyList<ITokenPattern> Patterns { get; init; } = new List<ITokenPattern>();

	public string Text { get; }

	public IReadOnlyList<IToken> Tokens => _tokens;
	
	public List<IToken> LexToEnd()
	{
		var list = new List<IToken>();
		
		for (int index = _currentPosition; index < Text.Length; index++)
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
		
		ITokenPattern? pattern = null;
		int position;

		for (position = _currentPosition; position < Text.Length; position++)
		{
			var @char = Text[position];

			var currentValue = value + @char;
			var formattedValue = currentValue.Trim();
			if (formattedValue == "")
			{
				value = currentValue;
				continue;
			}
			var currentPattern = Patterns.FirstOrDefault(e => e != null && e.IsPart(currentValue.Trim()), null);
			
			if (currentPattern is null)
			{
				if (pattern is null) break;
				
				var token = pattern.Part(value.Trim(), _currentPosition);
				_currentPosition += token.Lexeme.Length + (value.Length - value.Trim().Length);
				
				_tokens.Add(token);
				return token;
			}

			pattern = currentPattern;
			value = currentValue;
		}
		
		if (pattern is null) return null;
				
		var token2 = pattern.Part(value.Trim(), _currentPosition);
		_currentPosition += token2.Lexeme.Length + (value.Length - value.Trim().Length);
				
		_tokens.Add(token2);
		return token2;
	}
}