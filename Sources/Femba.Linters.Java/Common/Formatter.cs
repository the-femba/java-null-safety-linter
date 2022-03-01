using System.Text.RegularExpressions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Utils;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Formatter : IFormatter
{
	public string Format(string text) => 
		new Regex(@"\/\*(.|\n)*?\*\/|\/\/(.)+").Replace(text, "");
}