namespace Femba.Linters.Java.Parser.Interfaces;

public interface IAnalyzer
{
	IReadOnlyList<IFeature> Features { get; }

	IList<IAnalyzationResult> Analyze();
}