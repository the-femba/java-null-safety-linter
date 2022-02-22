namespace Femba.Linters.Java.Parser.Interfaces;

public interface IParser
{
	IReadOnlyList<IToken> Tokens { get; }
	
	IReadOnlySet<INode> NodesRegion { get; }
	
	IReadOnlyList<INode> NodesQueue { get; }

	INode? ParseNext();
	
	IList<INode> ParseToEnd();
}