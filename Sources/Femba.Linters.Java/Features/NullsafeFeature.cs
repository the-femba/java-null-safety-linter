using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Features;

public class NullsafeFeature : IFeature
{
	private List<string> _primitiveTypes = new()
	{
		"byte",
		"char",
		"string",
		"short",
		"int",
		"long",
		"float",
		"double",
		"boolean",
		"void"
	};
	
	public IList<IAnalyzationResult> Analize(INodeScope scope)
	{
		var results = new List<IAnalyzationResult>();
		
		var functions = scope.Select(e => (FunctionNode) e).ToList();

		foreach (var function in functions)
		{
			AnalizeFunction(function, functions, results);
		}

		return results;
	}

	private void AnalizeFunction(FunctionNode function, List<FunctionNode> functions, List<IAnalyzationResult> results)
	{
		var variableDeclarations = new Dictionary<VariableNode, bool>();
		
		foreach (var argument in function.Arguments)
		{
			AnalizeArgument(argument, variableDeclarations, results);
		}
		
		foreach (var node in function.Body)
		{
			if (node is VariableAssignmentNode variableAssignment)
			{
				AnalizeVariable(variableAssignment, variableDeclarations, results);
			}
		}
	}

	private void AnalizeArgument(VariableNode variable,
		Dictionary<VariableNode, bool> variableDeclarations, List<IAnalyzationResult> results)
	{
		if (!variableDeclarations.ContainsKey(variable))
		{
			variableDeclarations[variable] = IsNull(variable);
		}
		else
		{
			results.Add(new AnalizationResult("You cannot create the same variable multiple times.",
				variable.StartPosition, variable.EndPosition));
		}
	}

	private void AnalizeVariable(VariableAssignmentNode variableAssignment, 
		Dictionary<VariableNode, bool> variableDeclarations, List<IAnalyzationResult> results)
	{
		var variable = variableAssignment.Variable;

		var assignment = variableAssignment.Assignment;	
		
		if (variable.Type is not null && 
		         variableDeclarations.ContainsKey(variable))
		{
			results.Add(new AnalizationResult("You cannot create the same variable multiple times.",
				variableAssignment.StartPosition, variableAssignment.EndPosition));
			return;
		}

		if (variable.Type is not null)
		{
			variableDeclarations[variable] = IsNull(variable);
		}

		if (assignment is not LiteralNode literal) return;
		
		if (!variableDeclarations.ContainsKey(variable))
		{
			results.Add(new AnalizationResult("Attempt to assign a value to a variable that has not yet been initialized.",
				variableAssignment.StartPosition, variableAssignment.EndPosition));
			return;
		}

		if (literal.IsNull())
		{
			if (variableDeclarations[variable] is not false) return;
				
			results.Add(new AnalizationResult("A null literal is assigned to a variable that cannot be null.",
				variableAssignment.StartPosition, variableAssignment.EndPosition));
		}
	}

	private bool IsNull(VariableNode variable)
	{
		foreach (var annotation in variable.Annotations)
		{
			switch (annotation.Type.Name)
			{
				case "NotNull":
					return false;
				case "Nullable":
					return true;
			}
		}
		
		return !_primitiveTypes.Contains(variable.Name);
	}
}