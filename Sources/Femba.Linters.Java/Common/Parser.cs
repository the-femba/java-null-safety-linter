using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;
using Femba.Linters.Java.Parser.Patterns.Tokens;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Parser : IParser
{
	private int _currentPosition = 0;

	private readonly List<INodePattern> _patterns = new List<INodePattern>();
	
	public Parser(List<IToken> tokens)
	{
		Tokens = tokens.ToList();
	}

	public IReadOnlyList<IToken> Tokens { get; }

	public IReadOnlyList<INode> Nodes { get; } = new List<INode>();

	public IReadOnlyList<INodePattern> Patterns
	{
		get => _patterns;
		init
		{
			_patterns = value.ToList();
			foreach (var pattern in _patterns) pattern.Parser = this;
		}
	}

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

	public IReadOnlyList<INode> ParseToEnd()
	{
		var list = new List<INode>();

		INode? node;
		while ((node = ParseNext()) is not null)
		{
			list.Add(node);
		}

		return (IReadOnlyList<INode>) list;
	}
}