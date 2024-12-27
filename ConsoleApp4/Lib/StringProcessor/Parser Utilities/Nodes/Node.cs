namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes
{
    /// <summary>
    /// Represents an abstract node within an abstract syntax tree (AST) structure.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An AST is commonly used to represent expressions in a structured, 
    /// hierarchical form. Each <see cref="Node"/> in the AST corresponds to 
    /// a component of the expression—such as a literal number, a binary operation, 
    /// or a parenthesized sub-expression.
    /// </para>
    /// <para>
    /// The <see cref="Evaluate"/> method defines a contract that all concrete nodes 
    /// must fulfill to compute their numeric value. For example:
    /// <list type="bullet">
    /// <item><see cref="NumberNode"/> directly returns its numeric value.</item>
    /// <item><see cref="BinaryOperationNode"/> evaluates its left and right child nodes 
    /// and applies an operation (like addition or multiplication) to the resulting values.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// For an expression like "3 + 5":
    /// <code>
    /// Node three = new NumberNode(3);
    /// Node five = new NumberNode(5);
    /// Node addition = new BinaryOperationNode(three, five, (a, b) => a + b);
    /// double result = addition.Evaluate(); // Evaluates to 8
    /// </code>
    /// </example>
    public abstract class Node
    {
        /// <summary>
        /// Evaluates the node's value and returns the computed numeric result.
        /// </summary>
        /// <remarks>
        /// The evaluation logic depends on the specific node type. Concrete implementations must:
        /// <list type="bullet">
        /// <item>For literal value nodes (e.g., <see cref="NumberNode"/>), return their stored numeric value.</item>
        /// <item>For operation nodes (e.g., <see cref="BinaryOperationNode"/>), evaluate their child nodes 
        /// and apply the defined operation.</item>
        /// <item>For composite or special-purpose nodes, follow their custom evaluation logic as defined 
        /// by their respective classes.</item>
        /// </list>
        /// </remarks>
        /// <returns>
        /// A <see cref="double"/> representing the node's evaluated result.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// May be thrown if the node cannot produce a valid numeric result 
        /// (e.g., due to an invalid node type or missing data).
        /// </exception>
        public abstract double Evaluate();
    }
}