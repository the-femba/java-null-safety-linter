namespace Femba.Linters.Java.Parser.Interfaces;

public interface IParser
{
	IReadOnlyList<IToken> Tokens { get; }
	
	IReadOnlyList<INode> Nodes { get; }
	
	IReadOnlyList<INodePattern> Patterns { get; }

	INode? ParseNext();
	
	IReadOnlyList<INode> ParseToEnd();
}