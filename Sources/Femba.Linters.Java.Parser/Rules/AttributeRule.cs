using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Rules;

public sealed class AttributeRule : IRule
{
	public void Rule(IReadOnlyList<IToken> tokens)
	{
		for (int index = 0; index < tokens.Count; index++)
		{
			var token = tokens[index];

			if (!token.IsSymbol() || token.Lexeme != "@") continue;
			
			if (index >= tokens.Count - 1) throw new RuleLinterException(
				"Lexeme '@' is last in the tokens queue. After this lexeme must be type lexeme.");

			var nextToken = tokens[index + 1];
			
			if (!nextToken.IsType()) throw new RuleLinterException(
				"After '@' must be token with type 'Type' but in life other."); 
		}
	}
}