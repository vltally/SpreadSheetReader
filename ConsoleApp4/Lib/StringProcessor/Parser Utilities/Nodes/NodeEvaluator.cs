namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;

/// <summary>
/// Provides a straightforward implementation of the <see cref="INodeEvaluator"/> interface 
/// by delegating directly to the <see cref="Node"/> class's <see cref="Node.Evaluate"/> method.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="NodeEvaluator"/> acts as a thin wrapper that relies on each <see cref="Node"/> 
/// to define its own evaluation logic. This makes the evaluation process fully polymorphic; 
/// nodes such as <see cref="NumberNode"/> or <see cref="BinaryOperationNode"/> know how to 
/// compute their values internally, and the evaluator simply triggers that logic.
/// </para>
/// <para>
/// If future requirements demand more complex evaluation logic—such as caching results, 
/// collecting diagnostic information, or applying transformations before evaluation—this class 
/// can be extended without modifying the node classes.
/// </para>
/// </remarks>
public class NodeEvaluator : INodeEvaluator
{
    /// <summary>
    /// Evaluates the given <paramref name="node"/> by calling its <see cref="Node.Evaluate"/> method.
    /// </summary>
    /// <param name="node">
    /// The AST node to evaluate. It is expected that the node’s <see cref="Node.Evaluate"/> method 
    /// will return a numeric result representing the value of the sub-expression it models.
    /// </param>
    /// <returns>
    /// The numeric result of evaluating the specified node’s expression.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="node"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <paramref name="node"/> cannot produce a valid numeric result, 
    /// typically due to an invalid or unsupported node type or missing data.
    /// </exception>
    public double Evaluate(Node node)
    {
        if (node == null)
            throw new ArgumentNullException(nameof(node), "The node to evaluate cannot be null.");

        return node.Evaluate();
    }
}