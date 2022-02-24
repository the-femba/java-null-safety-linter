using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public class VariableAssignmentNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		var tokens = partition.ToList();
		var eqSymIndex = tokens.FindIndex(e => e.IsSymbol("="));

		if (eqSymIndex < 0) eqSymIndex = tokens.Count - 1;

		var leftSide = tokens.Take(eqSymIndex).ToList();

		if (!new VariableNodePattern().IsPart(leftSide)) return false;
		
		var endSymIndex = tokens.FindIndex(e => e.IsSymbol(";"));

		if (endSymIndex < 0) return true;

		if (endSymIndex - eqSymIndex <= 1) return false;

		return endSymIndex == tokens.Count - 1;
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var tokens = partition.ToList();

		if (!tokens.Last().IsSymbol(";")) throw new ParseLinterException(
			"Variable creation must end with a semicolon.", tokens.Last());
		
		var eqSymIndex = tokens.FindIndex(e => e.IsSymbol("="));
		
		if (eqSymIndex < 0) throw new ParseLinterException(
			"Can't find equal symbol.", tokens.First());
		
		var leftSide = tokens.Take(eqSymIndex).ToList();

		var variablePattern = new VariableNodePattern();
		
		if (!variablePattern.IsPart(leftSide)) throw new ParseLinterException(
			"Type and name or variable name are not appropriate for the format, include annotations.", tokens.First());

		var variable = variablePattern.Part(leftSide);
	
		var endRightSideIndex = tokens.Count - eqSymIndex - 2;
		if (endRightSideIndex < 1) throw new ParseLinterException(
			"Nothing is assigned to the variable.", tokens[eqSymIndex + 1]);
		
		var rightSide = tokens.GetRange(eqSymIndex + 1, endRightSideIndex);

		var assignment = new Common.Parser(rightSide, patterns: Parser!.Patterns.ToList()).ParseToEnd().FirstOrDefault();
		
		if (assignment is null) throw new ParseLinterException(
			"While parsing an assignment, the assigned could not be parsed.", tokens[eqSymIndex + 1]);
		
		if (assignment is FunctionNode) throw new ParseLinterException(
			"You can't assign a function declaration to a variable.", tokens[eqSymIndex + 1]);

		var eqNode = new VariableAssignmentNode((VariableNode) variable, assignment);
		variable.Parent = eqNode;
		assignment.Parent = eqNode;

		node = eqNode;

		return partition;
	}
}