using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class AnalizerTests
{
	[Fact]
	public void Test5()
	{
		var node = new Common.Parser("void myFun1(name1, name2) { int a = 2; }", new List<INodePattern>
		{
			new FunctionNodePattern(),
			new VariableInvokeNodePattern(),
			new FunctionInvokeNodePattern(),
			new VariableAssignmentNodePattern()
		}).ParseToEnd();
	}
}