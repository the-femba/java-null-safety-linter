using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Parser : IParser
{
	public Parser(string text)
	{
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

	public Parser(IReadOnlyList<IToken> tokens)
	{
		Tokens = tokens;
		NodesRegion = new HashSet<INode>();
		NodesQueue = new List<INode>();
	}

	public IReadOnlyList<IToken> Tokens { get; }

	public IReadOnlySet<INode> NodesRegion { get; }

	public IReadOnlyList<INode> NodesQueue { get; }

	public INode? ParseNext()
	{
		throw new NotImplementedException();
	}

	public IList<INode> ParseToEnd()
	{
		throw new NotImplementedException();
	}
}