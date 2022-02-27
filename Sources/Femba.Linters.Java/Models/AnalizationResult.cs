using Femba.Linters.Java.Parser.Enums;
using Femba.Linters.Java.Parser.Interfaces;

namespace Femba.Linters.Java.Parser.Models;

public sealed class AnalizationResult : IAnalyzationResult
{
	public AnalizationResult(string message, int startPosition, int endPosition)
	{
		StartPosition = startPosition;
		EndPosition = endPosition;
		Message = message;
	}

	public int StartPosition { get; }

	public int EndPosition { get; }

	public string Message { get; }

	public AnalyzationResultType Type { get; init; } = AnalyzationResultType.Error;
}