using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Utils;

namespace Femba.Linters.Java.Parser.Patterns.Nodes;

public sealed class IfElseNodePattern : NodePattern
{
	private readonly Token _openConditionToken = new Token(TokenType.Symbol, TokensNames.ArgumentOpenBasket);
	
	private readonly Token _closeConditionToken = new Token(TokenType.Symbol, TokensNames.ArgumentCloseBasket);
	
	private readonly Token _openBodyToken = new Token(TokenType.Symbol, TokensNames.BodyOpenBasket);
	
	private readonly Token _closeBodyToken = new Token(TokenType.Symbol, TokensNames.BodyCloseBasket);
	
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();
		
		if (tokens.Count == 0) return true;

		if (!tokens[0].IsKeyword(TokensNames.If) || !tokens[0].IsKeyword(TokensNames.Else)) return false;

		var openBodyBasketIndex = tokens.FindIndex(e => e.Equals(_openBodyToken));

		if (openBodyBasketIndex < 0) return true;
		
		var closeBodyBasketIndex = tokens.FindСlosingToken(_openBodyToken, _closeBodyToken, openBodyBasketIndex);

		return closeBodyBasketIndex > 0 && closeBodyBasketIndex == tokens.Count - 2;
	}

	public override List<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		// TODO: Добавить поддержку неправильного формата очереди токенов
		// TODO: так как поиск паттерна ноды примитивен и нет смысла егор усложнять.
		
		var tokens = partition.ToList();
		
		var condition = IsIfCondition(tokens) || IsElseIfCondition(tokens) 
			? new ConditionNode {Values = GetConditionTokens(tokens)} 
			: null;
		
		var body = new Common.Parser(GetBodyTokens(tokens)){Patterns = Parser!.Patterns}.ParseToEnd();

		node = UpdateBranchParents(new BranchNode(condition) {Body = body});
		return partition.ToList();
	}

	private BranchNode UpdateBranchParents(BranchNode node)
	{
		foreach (var bodyNode in node.Body) bodyNode.Parent = node;
		if (node.Condition is not null) node.Condition.Parent = node;
		return node;
	}

	private List<IToken> GetBodyTokens(List<IToken> tokens)
	{
		var openBodyBasketIndex = tokens.FindIndex(e => e.Equals(_openBodyToken));
		
		var closeBodyBasketIndex = 
			(int) tokens.FindСlosingToken(_openBodyToken, _closeBodyToken, openBodyBasketIndex)!;
		
		return tokens.GetRange(openBodyBasketIndex + 1,
			closeBodyBasketIndex - openBodyBasketIndex - 1);
	}
	
	private List<IToken> GetConditionTokens(List<IToken> tokens)
	{
		var openConditionBasketIndex = tokens.FindIndex(e => e.Equals(_openConditionToken));
		var closeConditionBasketIndex =
			(int) tokens.FindСlosingToken(_openConditionToken, _closeConditionToken, openConditionBasketIndex)!;

		return tokens.GetRange(openConditionBasketIndex + 1,
			closeConditionBasketIndex - openConditionBasketIndex - 1);
	}
	
	private bool IsIfCondition(List<IToken> tokens)
	{
		return tokens[0].IsKeyword(TokensNames.If);
	}

	private bool IsElseIfCondition(List<IToken> tokens)
	{
		return tokens[0].IsKeyword(TokensNames.Else) && tokens[1].IsKeyword(TokensNames.If);
	}
	
	private bool IsElseCondition(List<IToken> tokens)
	{
		return tokens[0].IsKeyword(TokensNames.Else) && !tokens[1].IsKeyword(TokensNames.If);
	}
}