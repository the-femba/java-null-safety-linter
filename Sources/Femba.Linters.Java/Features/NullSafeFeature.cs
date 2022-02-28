using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Models;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Patterns;
using FunctionsScope = System.Collections.Generic.Dictionary<Femba.Linters.Java.Parser.Nodes.FunctionNode, System.Collections.Generic.Dictionary<Femba.Linters.Java.Parser.Nodes.VariableNode, bool>>;

namespace Femba.Linters.Java.Parser.Features;

public sealed class NullSafeFeature : IFeature
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
	
	public IList<IAnalyzationResult> Analyze(IReadOnlyList<INode> scope)
	{
		var results = new List<IAnalyzationResult>();
		
		var functionsScope = new FunctionsScope();
		
		foreach (var node in scope)
		{
			var function = (FunctionNode) node;
			
			if (functionsScope.Keys.Any(e => e.Name == function.Name))
			{
				results.Add(new AnalizationResult("There is already a function with the same name.",
					function.StartPosition, function.EndPosition));
			}
			
			functionsScope[function] = new Dictionary<VariableNode, bool>();
			
			foreach (var argument in function.Arguments)
			{
				AnalyzeArgument(argument, function, functionsScope, results);
			}
		}
		
		foreach (var node in scope)
		{
			var function = (FunctionNode) node;
			
			AnalyzeBody(function, functionsScope, results);
		}

		return results;
	}

	private void AnalyzeBody(FunctionNode function, FunctionsScope scope, List<IAnalyzationResult> results)
	{
		foreach (var node in function.Body)
		{
			switch (node)
			{
				case VariableAssignmentNode variableAssignment:
					AnalyzeVariable(variableAssignment, function, scope, results);
					break;
				case FunctionInvokeNode functionInvoke:
					AnalyzeFunction(functionInvoke, function, scope, results);
					break;
			}
		}
	}

	private void AnalyzeFunction(FunctionInvokeNode function, FunctionNode current, FunctionsScope scope,
		List<IAnalyzationResult> results)
	{
		if (scope.Keys.All(e => e.Name != function.Function.Name))
		{
			results.Add(new AnalizationResult("The function is not available in the current scope. " +
			                                  "All default arguments can be null and cannot parse the number of arguments.",
				function.StartPosition, function.EndPosition)
			{
				Type = AnalyzationResultType.Hint,
			});
		}
		else
		{
			if (function.Values.Count != current.Arguments.Count)
			{
				results.Add(new AnalizationResult("The number of arguments passed to the function does not match the number of arguments received",
					function.StartPosition, function.EndPosition));
				return;
			}

			for (int index = 0; index < current.Arguments.Count; index++)
			{
				var argument = current.Arguments[index];
				if (function.Values[index] is not VariableInvokeNode variableInvoke) continue;
				var valueInvoke = (VariableInvokeNode) function.Values[index];
				
				if (scope[current].All(e => e.Key.Name != variableInvoke.Variable.Name))
				{
					results.Add(new AnalizationResult("Can't find variable in current scope.",
						valueInvoke.StartPosition, valueInvoke.EndPosition));
					continue;
				}
				
				var (value, isNull) = scope[current]
					.First(e => e.Key.Name == variableInvoke.Variable.Name);
				
				if (value.Type?.Name != argument.Type?.Name)
				{
					results.Add(new AnalizationResult("The type of the value passed to the " +
					                                  "function do not match the type of the function argument",
						value.StartPosition, value.EndPosition));
					continue;
				}
				
				if (isNull && !IsNull(argument))
				{
					results.Add(new AnalizationResult("Cannot pass null to a function where the argument is not null.",
						value.StartPosition, value.EndPosition)
					{
						Type = AnalyzationResultType.Warning
					});
				}
			}
		}
	}

	private void AnalyzeArgument(VariableNode variable, FunctionNode current, FunctionsScope scope,
		List<IAnalyzationResult> results)
	{
		if (scope[current].Keys.All(e => e.Name != variable.Name))
		{
			scope[current][variable] = IsNull(variable);
		}
		else
		{
			results.Add(new AnalizationResult("You cannot create the same arguments multiple times.",
				variable.StartPosition, variable.EndPosition));
		}
	}

	private void AnalyzeVariable(VariableAssignmentNode variableAssignment, 
		FunctionNode current, FunctionsScope scope, List<IAnalyzationResult> results)
	{
		var variable = variableAssignment.Variable;

		var assignment = variableAssignment.Assignment;	
		
		if (variable.Type is not null && scope[current].Keys.Any(e => e.Name == variable.Name))
		{
			results.Add(new AnalizationResult("You cannot create the same variable multiple times.",
				variableAssignment.StartPosition, variableAssignment.EndPosition));
		}

		if (variable.Type is not null)
		{
			scope[current][variable] = IsNull(variable);
		}

		if (assignment is not LiteralNode literal) return;
		
		if (scope[current].All(e => e.Key.Name != variable.Name))
		{
			results.Add(new AnalizationResult("Attempt to assign a value to a variable that has not yet been initialized.",
				variableAssignment.StartPosition, variableAssignment.EndPosition));
		}

		if (literal.IsNull())
		{
			results.Add(new AnalizationResult("A null literal is assigned to a variable that cannot be null.",
				variableAssignment.StartPosition, variableAssignment.EndPosition)
			{
				Type = AnalyzationResultType.Warning
			});
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