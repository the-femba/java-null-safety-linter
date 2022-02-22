using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Parser : IParser
{
	private List<IToken> _currentTokens;
	
	public Parser(string text, List<INodePattern> patterns)
	{
		Patterns = patterns;
		foreach (var pattern in patterns) pattern.Parser = this;
		
		var tokens = new Lexer(new Formatter().Format(text), patterns: new HashSet<ITokenPattern>
		{
			new NumberLiteralPattern(),
			new StringLiteralTokenPattern(),
			new CharLiteralTokenPattern(),
			new KeywordPattern(),
			new NamePattern(),
			new SymbolPattern(),
			new TypePattern()
		}).LexToEnd();
		
		_currentTokens = tokens.ToList();
		Tokens = tokens.ToList();
	}

	public Parser(List<IToken> tokens, List<INodePattern> patterns)
	{
		Tokens = tokens.ToList();
		_currentTokens = tokens.ToList();
		Patterns = patterns;
		foreach (var pattern in patterns) pattern.Parser = this;
	}

	public IReadOnlyList<IToken> Tokens { get; }

	public IReadOnlyList<INode> Nodes { get; } = new List<INode>();

	public IReadOnlyList<INodePattern> Patterns { get; }

	public INode? ParseNext()
	{
		List<IToken>? lastQueue = null;
		INodePattern? lastPattern = null;
		
		int index;
		for (index = 0; index < _currentTokens.Count; index++)
		{
			var token = Tokens[index];
			
			lastQueue ??= new List<IToken>();
			
			var queue = lastQueue;
			queue.Add(token);

			var pattern = Patterns.FirstOrDefault(e => e.IsPart(queue));

			if (index == _currentTokens.Count - 1)
			{
				lastPattern = pattern;
				pattern = null;
			}
			
			if (pattern is null)
			{
				if (lastPattern is null) return null;
				
				var usedTokens = lastPattern.Part(lastQueue, out var node);
				foreach (var usedToken in usedTokens) _currentTokens.Remove(usedToken);
				return node;
			}

			lastPattern = pattern;
			lastQueue = queue;
		}

		return null;
	}

	public IList<INode> ParseToEnd()
	{
		var list = new List<INode>();
		
		for (int index = 0; index <= _currentTokens.Count; index++)
		{
			var token = ParseNext();
			if (token is null) break;
			list.Add(token);
		}

		return list;
	}
}