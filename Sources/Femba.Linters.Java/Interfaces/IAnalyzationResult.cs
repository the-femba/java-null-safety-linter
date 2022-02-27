using Femba.Linters.Java.Parser.Enums;

namespace Femba.Linters.Java.Parser.Interfaces;

public interface IAnalyzationResult
{
	int StartPosition { get; }
	int EndPosition { get; }
	string Message { get; }
	AnalyzationResultType Type { get; }
}