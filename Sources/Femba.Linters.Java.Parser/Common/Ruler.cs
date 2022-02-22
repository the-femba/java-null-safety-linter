using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Ruler : IRuler
{
	private HashSet<IRule> _rules;
	
	public Ruler(HashSet<IRule>? rules = null)
	{
		_rules = rules ?? new HashSet<IRule>();
	}

	public IList<IRule> Rules => _rules.ToList();

	public void Rule(List<IToken> tokens)
	{
		foreach (var rule in _rules) rule.Rule(tokens);
	}
}