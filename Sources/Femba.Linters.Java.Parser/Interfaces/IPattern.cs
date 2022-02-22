namespace Femba.Linters.Java.Parser.Interfaces;

public interface IPattern<in TPartition, out TResult>
{
	bool IsPart(TPartition partition);

	TResult Part(TPartition partition);
}