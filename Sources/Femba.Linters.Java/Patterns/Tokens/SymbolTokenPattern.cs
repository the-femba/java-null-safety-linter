using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Patterns.Tokens;

public sealed class SymbolPattern : TokenRegexPattern
{
	public SymbolPattern()
		: base(TokenType.Symbol, new Regex(@"^([-+=<>,\.)({}*\/;:@]|==|>=|<=)$")) { }
}