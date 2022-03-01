using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Analyzer : IAnalyzer
{
	private readonly IReadOnlyList<INode> _scope;

	public Analyzer(IReadOnlyList<INode> scope)
	{
		_scope = scope;
	}

	public IReadOnlyList<IFeature> Features { get; init; } = new List<IFeature>();

	public List<IAnalyzationResult> Analyze()
	{
		var list = new List<IAnalyzationResult>();
		foreach (var feature in Features) list.AddRange(feature.Analyze(_scope));
		return list;
	}
}