using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Parser : IParser
{
	private int _currentPosition = 0;
	
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
		
		Tokens = tokens.ToList();
	}

	public Parser(List<IToken> tokens, List<INodePattern> patterns)
	{
		Tokens = tokens.ToList();
		Patterns = patterns;
		foreach (var pattern in patterns) pattern.Parser = this;
	}

	public IReadOnlyList<IToken> Tokens { get; }

	public IReadOnlyList<INode> Nodes { get; } = new List<INode>();

	public IReadOnlyList<INodePattern> Patterns { get; }

	public INode? ParseNext()
	{
		if (Tokens.Count == 0) return null;
		
		List<IToken>? lastQueue = null;
		INodePattern? lastPattern = null;
		INodePattern? pattern = null;
		
		int index;
		for (index = _currentPosition; index < Tokens.Count; index++)
		{
			var token = Tokens[index];
			
			lastQueue ??= new List<IToken>();
			
			var queue = lastQueue.ToList();
			queue.Add(token);

			pattern = Patterns.FirstOrDefault(e => e.IsPart(queue));
			
			if (pattern is null)
			{
				if (lastPattern is null) return null;
				
				var usedTokens = lastPattern.Part(lastQueue, out var node);
				_currentPosition += usedTokens.Count;
				return node;
			}

			lastPattern = pattern;
			lastQueue = queue;
		}

		if (lastPattern is not null)
		{
			var usedTokens = lastPattern.Part(lastQueue!, out var node);
			_currentPosition += usedTokens.Count;
			return node;
		}

		return null;
	}

	public INodeScope ParseToEnd()
	{
		var list = new List<INode>();

		INode? node;
		while ((node = ParseNext()) is not null)
		{
			list.Add(node);
		}

		return (INodeScope) list;
	}
}