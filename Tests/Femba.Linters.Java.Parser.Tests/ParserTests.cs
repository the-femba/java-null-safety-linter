using Femba.Linters.Java.Parser.Nodes;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class ParserTests
{
	[Fact]
	public void Test1()
	{
		var node = new FunctionNode(0, 0, 
			new TypeNode(0, 0, false, "string"), 
			"myFunc1", arguments: 
			new List<VariableNode>
		{
			new VariableNode(0, 0, 
				new TypeNode(0, 0, false, "string"), 
				"myArg1"),
			new VariableNode(0, 0, 
				new TypeNode(0, 0, false, "Point"), 
				"myArg2"),
			new VariableNode(0, 0, 
				new TypeNode(0, 0, true, "Point"), 
				"myArg2", 
				annotations: new List<AnnotationNode>
			{
				new AnnotationNode(0, 0, new TypeNode(0, 0, false, "Nullable"))
			}),
		});
	}

	[Fact]
	public void Test2()
	{
		var stringType = new TypeNode(0, 0, false, "string");

		var myFuncFunction = new FunctionNode(0, 0, stringType, "myFunc");

		var myFuncVariable = new VariableNode(0, 0, stringType, "value");

		var invokeMethodAndAfterVariable =
			new FunctionInvokeNode(0, 0, myFuncFunction, 
				after: new VariableInvokeNode(0, 0, myFuncVariable));
		
		var resultVariable = new VariableNode(0, 0, stringType, "result");

		var assignToResultVariable = new VariableAssignmentNode(0, 0, resultVariable, invokeMethodAndAfterVariable);
			
		var node = assignToResultVariable;
	}
}