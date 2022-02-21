using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Common;

public sealed class Ruler : IRuler
{
	private List<IRule> _rules;
	
	public Ruler()
	{
		_rules = new List<IRule>();
	}
	
	public Ruler(List<IRule> rules)
	{
		_rules = rules;
	}
	
	public Ruler(params IRule[] rules)
	{
		_rules = rules.ToList();
	}

	public IList<IRule> Rules => _rules;

	public void Rule(List<IToken> tokens)
	{
		foreach (var rule in _rules) rule.Rule(tokens);
	}
}