namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations.Supported;

public class SubtractionOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Low;

    public char Symbol { get; } = '-';
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand - rightOperand;
}