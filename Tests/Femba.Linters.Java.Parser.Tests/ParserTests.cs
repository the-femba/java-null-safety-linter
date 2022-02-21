using Femba.Linters.Java.Parser.Nodes;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class ParserTests
{
	[Fact]
	public void Test()
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
}