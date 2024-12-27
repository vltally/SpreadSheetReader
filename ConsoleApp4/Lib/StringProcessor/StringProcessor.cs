using ConsoleApp4.Lib.StringProcessor.Evaluator;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;
using ConsoleApp4.Lib.StringProcessor.Tokenizer;
using ConsoleApp4.Lib.StringProcessor.Validator;


namespace ConsoleApp4.Lib.StringProcessor;

public class StringProcessor
{
   
    public double ProcessString(string input)
    {
        List<IValidator> validators = new()
        {
            new LetterValidator(),
            new ParenValidator()
        };

        validators.ForEach(validator => validator.ValidateExpression(input));


        ITokenizer tokenizer = new ExpressionTokenizer();
        IParser parser = new ExpressionParser();
        INodeEvaluator nodeEvaluator = new NodeEvaluator();
        IExpressionEvaluator evaluator = new ExpressionEvaluator(tokenizer, parser, nodeEvaluator);

        try
        {
            //Console.WriteLine($"Input: {input}");
            double result = evaluator.Evaluate(input);
            //Console.WriteLine($"Result: {Math.Round(result, 2)}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while evaluating the expression:");
            Console.WriteLine(ex.Message);
        }

        return 0;


    }

}