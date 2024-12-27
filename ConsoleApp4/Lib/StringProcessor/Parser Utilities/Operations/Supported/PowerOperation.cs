namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations.Supported;

public sealed class PowerOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.High;

    public char Symbol { get; } = '^';
    public Func<double, double, double> Execute => Math.Pow;  
}

