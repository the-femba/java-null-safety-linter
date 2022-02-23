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
		var text = "int myFunc1() \{ int myFnc2() { int myFnc2() { } } int myFnc2() { } } int myFnc2() { } int myFunc3() { } int myFunc4() { } ";
		
		var parser = new Common.Parser(text, new List<INodePattern>
		{
			new FunctionNodePattern()
		});

		var node = parser.ParseToEnd();
	}
}