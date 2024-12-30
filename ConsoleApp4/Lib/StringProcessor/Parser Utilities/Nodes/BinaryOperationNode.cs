namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes
{
   
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