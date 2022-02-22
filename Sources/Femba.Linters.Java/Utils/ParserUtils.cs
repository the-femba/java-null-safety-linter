using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Utils;

public static class ParserUtils
{
	public static int? FindCloseToken(IToken open, IToken close, List<IToken> tokens, int from = 0)
	{
		var counter = 0;
		
		for (int index = from; index < tokens.Count; index++)
		{
			var token = tokens[index];
			
			if (token == open) counter++;
			else if (token == close) counter--;

			if (counter == 0)
			{
				return index;
			}
		}

		return null;
	}
}