using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Patterns;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class ParserTests
{
	[Fact]
	public void Test1()
	{
		var text = "int myFun1(int a, int b) { void test() {} } int myFun2(string text, int b) { }";
		
		var parser = new Common.Parser(text, new List<INodePattern>
		{
			new FunctionNodePattern(),
			new AnnotationNodePattern(),
			new VariableNodePattern(),
			new TypeNodePattern()
		});

		var node = parser.ParseToEnd();
	}
}