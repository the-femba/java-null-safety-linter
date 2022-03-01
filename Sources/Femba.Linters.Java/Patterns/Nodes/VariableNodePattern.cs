using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;
using Femba.Linters.Java.Parser.Utils;

namespace Femba.Linters.Java.Parser.Patterns.Nodes;

public sealed class VariableNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		if (partition.Count == 0) return true;
		
		// TODO: Add Attribute support.
		if (partition.Count == 1 && partition[0].IsSymbol(TokensNames.Dog)) return true;
		if (partition.Count == 1 && partition[0].IsType()) return true;

		if (partition.Count == 1) return true;

		if (partition[^1].IsSymbol(TokensNames.ArgumentOpenBasket)) return false;

		if (partition[0].IsSymbol(TokensNames.Dog) && partition[1].IsType()) return true;
		if (partition[0].IsType() && partition[1].IsName()) return true;
		
		return false;
	}

	public override List<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
	{
		var tokens = partition.ToList();
		
		var type = tokens[^2];
		var name = tokens[^1];
		
		tokens.RemoveRange(tokens.Count - 2, 2);

		var typeNode = new TypeNodePattern().Part(new []{type});

		var annotations = new List<AnnotationNode>();

		if (tokens.Count > 0)
		{
			for (int i = 1; i < tokens.Count; i += 2)
			{
				var annotationTokens = tokens.GetRange(i - 1, 2);

				var pattern = new AnnotationNodePattern();
				
				if (!pattern.IsPart(annotationTokens)) continue;
				
				var annotation = pattern.Part(annotationTokens);
				annotations.Add((AnnotationNode) annotation);
			}
		}
		
		var variable = new VariableNode((TypeNode) typeNode, name.Lexeme)
		{
			StartPosition = type.Position,
			EndPosition = name.Position + name.Lexeme.Length,
			Annotations = annotations
		};
		
		typeNode.Parent = variable;

		foreach (var annotation in annotations)
		{
			annotation.Parent = variable;
		}

		node = variable;

		return partition.ToList();
	}
}