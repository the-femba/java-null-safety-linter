using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Formatter : IFormatter
{
	public string Format(string text)
	{
		return new Regex(@"\/\*(.|\n)*?\*\/|\/\/(.)+").Replace(text, "");
	}
}