
using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Features;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Patterns;
using Femba.Linters.Java.Parser.Patterns.Nodes;
using Femba.Linters.Java.Parser.Patterns.Tokens;
using Newtonsoft.Json;

try
{
	var path = args[0];
	
	var text = File.ReadAllText(path);

	text = new Formatter().Format(text);

	var tokens = new Lexer(text)
	{
		Patterns = new ITokenPattern[]
		{
			new NumberLiteralPattern(),
			new NullLiteralTokenPattern(),
			new StringLiteralTokenPattern(),
			new CharLiteralTokenPattern(),
			new KeywordPattern(),
			new NamePattern(),
			new SymbolPattern(),
			new TypePattern()
		}
	}.LexToEnd();

	var nodes = new Parser(tokens.ToList())
	{
		Patterns = new INodePattern[]
		{
			new LiteralNodePattern(),
			new FunctionNodePattern(),
			new VariableInvokeNodePattern(),
			new FunctionInvokeNodePattern(),
			new VariableAssignmentNodePattern()
		}
	}.ParseToEnd();

	var results = new Analyzer(nodes)
	{
		Features = new IFeature[]
		{
			new NullSafeFeature()
		}
	}.Analyze();

	text = JsonConvert.SerializeObject(results, Formatting.Indented);

	File.WriteAllText(path + ".analyze.json", text);
	Console.WriteLine($"// Analize result.\n{text}");
}
catch (Exception exception)
{
	Console.Write($"ERROR <{exception.GetType().Name}> '{exception.Message}'");
}
