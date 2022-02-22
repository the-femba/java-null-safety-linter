using Femba.Linters.Java.Parser.Common;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class FormatterTests
{
	[Fact]
	public void TestReplaceBasketComment()
	{
		var text = "beb/*test*/ra";
		
		var formattedText = new Formatter().Format(text);
		
		Assert.Equal("bebra", formattedText);
	}
	
	[Fact]
	public void TestReplaceNotBasketComment()
	{
		var text = "bebra// test";
		
		var formattedText = new Formatter().Format(text);
		
		Assert.Equal("bebra", formattedText);
	}
}