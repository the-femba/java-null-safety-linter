using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Features;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class AnalizerTests
{
	[Fact]
	public void Test5()
	{
		var node = new Common.Parser("void myFun1(int name1, Point name2) { int bebra2 = null; }", new List<INodePattern>
		{
			new LiteralNodePattern(),
			new FunctionNodePattern(),
			new VariableInvokeNodePattern(),
			new FunctionInvokeNodePattern(),
			new VariableAssignmentNodePattern()
		}).ParseToEnd();

		var analizer = new Analyzer(node, new[] {new NullsafeFeature()});
		var results = analizer.Analyze();
	}
}