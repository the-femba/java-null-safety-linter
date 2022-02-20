using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class SymbolPattern : LexemeRegexPattern
{
	public SymbolPattern()
		: base(TokenType.Symbol, new Regex(@"^[-+=<>)({}*\/;@]$")) { }
}