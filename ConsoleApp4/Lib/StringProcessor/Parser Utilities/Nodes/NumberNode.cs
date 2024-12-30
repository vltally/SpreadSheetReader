namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;


public class NumberNode(double value) : Node
{
    /// <summary>
    /// Evaluates this node by returning the numeric value it represents.
    /// </summary>
    /// <returns>
    /// A <see cref="double"/> equal to the <paramref name="value"/> stored in the node.
    /// </returns>
    public override double Evaluate()
    {
        return value;
    }
}