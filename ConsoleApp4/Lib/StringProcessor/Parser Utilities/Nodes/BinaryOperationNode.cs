namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes
{
    /// <summary>
    /// Represents a binary operation node in an abstract syntax tree (AST). 
    /// This node applies a binary operation (such as addition, subtraction, 
    /// multiplication, division, etc.) to two operand sub-expressions.
    /// </summary>
    /// <remarks>
    /// A <see cref="BinaryOperationNode"/> encapsulates a binary function 
    /// (<paramref name="operation"/>) that takes two numeric values and 
    /// produces a single numeric result. It evaluates its left and right 
    /// child nodes and then applies the operation.
    /// </remarks>
    /// <example>
    /// For an expression like "3 + 5", the parser might create:
    /// <code>
    /// var leftNode = new NumberNode(3);
    /// var rightNode = new NumberNode(5);
    /// var additionOp = (double a, double b) => a + b;
    /// var node = new BinaryOperationNode(leftNode, rightNode, additionOp);
    /// double result = node.Evaluate(); // result = 8
    /// </code>
    /// </example>
    /// <param name="left">
    /// The left operand node. This node should return a numeric value when evaluated.
    /// </param>
    /// <param name="right">
    /// The right operand node. This node should also return a numeric value when evaluated.
    /// </param>
    /// <param name="operation">
    /// A function that takes two <see cref="double"/> values (from the left and right nodes) 
    /// and returns a <see cref="double"/> result.
    /// </param>
    public class BinaryOperationNode(Node left, Node right, Func<double, double, double> operation) : Node
    {
        /// <summary>
        /// Evaluates the binary operation by first evaluating the left and right operand nodes, 
        /// then applying the provided operation function to these values.
        /// </summary>
        /// <returns>
        /// The numeric result of applying the binary operation to the evaluated values 
        /// of the left and right child nodes.
        /// </returns>
        public override double Evaluate()
        {
            double leftVal = left.Evaluate();
            double rightVal = right.Evaluate();
            return operation(leftVal, rightVal);
        }
    }
}