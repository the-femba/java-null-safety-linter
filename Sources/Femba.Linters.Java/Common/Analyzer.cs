using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Analyzer : IAnalyzer
{
	private readonly INodeScope _scope;

	public Analyzer(INodeScope scope, IReadOnlyList<IFeature>? features = null)
	{
		_scope = scope;
		Features = features ?? new List<IFeature>();
	}

	public IReadOnlyList<IFeature> Features { get; }

	public IList<IAnalyzationResult> Analyze()
	{
		var list = new List<IAnalyzationResult>();
		foreach (var feature in Features) list.AddRange(feature.Analize(_scope));
		return list;
	}
}