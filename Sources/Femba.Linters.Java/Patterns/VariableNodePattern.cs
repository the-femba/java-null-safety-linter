using Femba.Linters.Java.Parser.Common;
using Femba.Linters.Java.Parser.Extensions;
using Femba.Linters.Java.Parser.Interfaces;
using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Patterns;

public sealed class VariableNodePattern : NodePattern
{
	public override bool IsPart(IReadOnlyList<IToken> partition)
	{
		// TODO: Add Attribute support.
		if (partition.Count == 1 && partition[0].IsType()) return true;
		if (partition.Count == 2 && partition[0].IsType() && partition[1].IsName()) return true;
		if (partition.Count == 3 && partition[0].IsType() && partition[1].IsName() &&
		    !(partition[2].IsSymbol() && partition[2].Lexeme == "(")) return true;
		return false;
	}

	public override IReadOnlyList<IToken> Part(IReadOnlyList<IToken> partition, out INode node)
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

		return partition;
	}
}