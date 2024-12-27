namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations.Supported;

public sealed class AdditionOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Low;
    public char Symbol { get; } = '+';
    public Func<double, double, double> Execute { get; } = (leftOperand, rightOperand) => leftOperand + rightOperand;
}