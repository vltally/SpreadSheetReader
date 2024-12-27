namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations.Supported;

public class ModulusOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Medium;

    public char Symbol { get; } = '%';
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand % rightOperand;
}