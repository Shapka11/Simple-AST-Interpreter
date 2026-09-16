namespace SimpleASTInterpretator.Core.Evaluators;

public interface IEvaluator
{
    public long Evaluate(long left, long right, string operation);
}