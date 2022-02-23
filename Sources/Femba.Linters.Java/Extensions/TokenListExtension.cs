using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Extensions;

public static class TokenListExtension
{
	public static int? FindСlosingToken(this List<IToken> tokens, IToken open, IToken close, int from = 0)
	{
		var counter = 0;
		
		for (int index = from; index < tokens.Count; index++)
		{
			var token = tokens[index];
			
			if (token.Equals(open)) counter++;
			else if (token.Equals(close)) counter--;

			if (counter == 0) return index;
		}

		return null;
	}

	public static List<List<IToken>> SplitTokens(this List<IToken> tokens, Func<IToken, bool> func)
	{
		var splitedList = new List<List<IToken>>();

		List<IToken>? splitingList = null;

		foreach (var token in tokens)
		{
			splitingList ??= new List<IToken>();

			if (func(token))
			{
				splitedList.Add(splitingList);
				splitingList = null;
			}
			else splitingList.Add(token);
		}

		if (splitingList is not null) splitedList.Add(splitingList);

		return splitedList;
	}

	public static bool SameAtStartWith(this List<IToken> tokens, List<IToken> sameWith)
	{
		throw new NotImplementedException();
	}
}