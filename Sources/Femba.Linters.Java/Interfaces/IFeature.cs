namespace Femba.Linters.Java.Parser.Interfaces;

public interface IFeature
{
	IList<IAnalyzationResult> Analize(IReadOnlyList<INode> scope);
}