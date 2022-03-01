using Femba.Linters.Java.Parser.Nodes;

namespace Femba.Linters.Java.Parser.Utils;

public static class UpdateParentsUtils
{
	public static BranchNode UpdateBranch(BranchNode node)
	{
		foreach (var bodyNode in node.Body) bodyNode.Parent = node;
		if (node.Condition is not null) node.Condition.Parent = node;
		return node;
	}
	
	public static AnnotationNode UpdateAnnotation(AnnotationNode node)
	{
		node.Type.Parent = node;
		return node;
	}
}