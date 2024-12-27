namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes
{
    /// <summary>
    /// Defines a contract for evaluating an abstract syntax tree (AST) node to produce a numeric result.
    /// </summary>
    /// <remarks>
    /// Implementations of <see cref="INodeEvaluator"/> are responsible for interpreting 
    /// <see cref="Node"/> instances—such as <see cref="NumberNode"/> or 
    /// <see cref="BinaryOperationNode"/>—and computing their numeric results. 
    /// This interface abstracts away the specifics of node evaluation, enabling 
    /// flexible and testable evaluation strategies.
    /// </remarks>
    /// <example>
    /// Consider an AST representing "3 + (5 * 2)":
    /// <code>
    /// var three = new NumberNode(3);
    /// var five = new NumberNode(5);
    /// var two = new NumberNode(2);
    /// var multiply = new BinaryOperationNode(five, two, (a, b) => a * b);
    /// var plus = new BinaryOperationNode(three, multiply, (a, b) => a + b);
    /// 
    /// INodeEvaluator evaluator = new NodeEvaluator(); // Example implementation
    /// double result = evaluator.Evaluate(plus); // result should be 13
    /// </code>
    /// </example>
    public interface INodeEvaluator
    {
        /// <summary>
        /// Evaluates the given <paramref name="node"/> and returns its numeric result.
        /// </summary>
        /// <param name="node">A node in the AST, representing a portion of an expression.</param>
        /// <returns>
        /// A <see cref="double"/> that represents the computed value of the AST 
        /// defined by <paramref name="node"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="node"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the node cannot be evaluated due to invalid or unsupported node types.
        /// </exception>
        double Evaluate(Node node);
    }
}