using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;
using Femba.Linters.Java.Parser.Rules;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Parser : IParser
{
	private readonly List<IToken> _tokens;

	public Parser(string text)
	{
		var formattedText = new Formatter().Format(text);
		
		var tokens = new Lexer(formattedText, patterns: new HashSet<ITokenPattern>
		{
			new NumberLiteralPattern(),
			new StringLiteralTokenPattern(),
			new CharLiteralTokenPattern(),
			new KeywordPattern(),
			new NamePattern(),
			new SymbolPattern(),
			new TypePattern()
		}).LexToEnd();
		
		new Ruler(new HashSet<IRule>
		{
			new AnnotationRule()
		}).Rule(tokens.ToList());
		
		_tokens = tokens.ToList();
	}
	
	public Parser(List<IToken> tokens)
	{
		_tokens = tokens;
	}
	
	public IList<INode> Parse()
	{
		throw new NotImplementedException();
	}
}