using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Exceptions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Rules;
using Xunit;

namespace Femba.Linters.Java.Parser.Tests;

public class AttributeRulerTests
{
	private readonly Ruler _ruler = new Ruler(new HashSet<IRule> {new AnnotationRule()});
	
	[Fact]
	public void TestNormalAttributeState()
	{
		var text = "@NotNull int test;";
		
		var lexer = new Lexer(text);
		
		_ruler.Rule(lexer.LexToEnd().ToList());
	}
	
	[Fact]
	public void TestNormalAttributeStateWithoutTypeAfter()
	{
		var text = "@NotNull";
		
		var lexer = new Lexer(text);
		
		_ruler.Rule(lexer.LexToEnd().ToList());
	}
	
	[Fact]
	public void TestNormalAttributeStateWithOtherTypeAfter()
	{
		var text = "@23.4 int";
		
		var lexer = new Lexer(text);
		
		Assert.Throws<ParseLinterException>(() =>
			_ruler.Rule(lexer.LexToEnd().ToList()));
	}
}