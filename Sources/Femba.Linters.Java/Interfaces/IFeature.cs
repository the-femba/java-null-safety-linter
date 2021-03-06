namespace Femba.Linters.Java.Parser.Interfaces;

public interface IFeature
{
	IList<IAnalyzationResult> Analyze(IReadOnlyList<INode> scope);
}