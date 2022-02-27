using System.Linq;
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class FunctionNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();
		
		if (tokens.Count == 1)
			return tokens[0].IsType();
		if (tokens.Count == 2)
			return tokens[0].IsType() && tokens[1].IsName();
		if (!tokens[0].IsType() || !tokens[1].IsName() || !tokens[2].IsSymbol("(")) return false;

		if (tokens.Count == 4) return true;

		var closeArgBasket = tokens.FindIndex(e => e.IsSymbol(")"));
		
		if (closeArgBasket < 0) return true;

		var openBodyBasket = tokens.FindIndex(e => e.IsSymbol("{"));

		if (openBodyBasket < 0) return true;
		
		var openToken = new Token(TokenType.Symbol, "{");
		var closeToken = new Token(TokenType.Symbol, "}");

		var closedBodyToken = tokens.FindСlosingToken(openToken, closeToken, openBodyBasket);

		if (closedBodyToken is null) return true;

		var r = closedBodyToken == tokens.Count - 1;
		return r;
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var tokens = partition.ToList();
		
		var type = new TypeNodePattern().Part(new []{tokens[0]});
		
		var name = tokens[1].Lexeme;

		var arguments = GetArgumentsTokens(tokens).Select(e =>
		{
			return new VariableNodePattern().Part(e);
		}).ToList();
		
		var body = new Common.Parser(GetBodyTokens(tokens), Parser!.Patterns.ToList())
			.ParseToEnd().ToList();
		
		body.ForEach(e =>
		{
			if (e is FunctionNode) throw new ParseLinterException(
				"Java does not support nested functions.", partition.First());
		});

		var func = new FunctionNode((TypeNode) type, name)
		{
			// FIXME: 
			Arguments = arguments.Select(e => (VariableNode) e).ToList(),
			Body = (IReadOnlyList<INode>) body,
			StartPosition = type.StartPosition,
			// TODO: Added true end position relative to the closed function basket.
			EndPosition = tokens.Last().Position
		};

		type.Parent = func;
		arguments.ForEach(e => e.Parent = func);
		body.ForEach(e => e.Parent = func);

		node = func;
		
		return partition;
	}

	private static List<IToken> GetBodyTokens(List<IToken> tokens)
	{
		var openToken = new Token(TokenType.Symbol, "{");
		var closeToken = new Token(TokenType.Symbol, "}");
		
		var openIndex = tokens.FindIndex(e => e.IsSymbol("{"));
		
		var closeIndex = (int) tokens.FindСlosingToken(openToken, closeToken, openIndex)!;

		if (closeIndex - openIndex <= 1) return new List<IToken>();

		return tokens.GetRange(openIndex + 1, closeIndex - openIndex - 1);
	}

	private static List<List<IToken>> GetArgumentsTokens(List<IToken> tokens)
	{
		var closeIndex = tokens.FindIndex(e => e.IsSymbol(")"));

		if (closeIndex == 3) return new List<List<IToken>>();
		
		return tokens.GetRange(3, closeIndex - 3)
			.SplitTokens(e => e.IsSymbol(",") || e.IsSymbol(")"));
	}
}