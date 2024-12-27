namespace ConsoleApp4.Lib.StringProcessor.Validator;

public class ParenValidator : IValidator
{
    
    private const string InvalidNumberPositionBeforeParen =
        "The opening parenthesis cannot be preceded by a number or a closing parenthesis.";
    private const string UnpairedClosingBracket = "Unpaired closing bracket";
    private const string InvalidContentBetweenBrackets = "Empty or invalid content between brackets";
    private const string InvalidBracketsClosure = "Not all brackets are closed";
    
    public bool ValidateExpression(string input)
    {
        try
        {
            Stack<char> stack = new Stack<char>();
            int lastOpenIndex = -1; 

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c == '(')
                {
                   
                    if (i > 0)
                    {
                        char prevChar = input[i - 1];
                        if (char.IsDigit(prevChar) || prevChar == ')')
                        {
                            throw new ArgumentException(InvalidNumberPositionBeforeParen);
                        }
                    }

                 
                    stack.Push(c);
                    lastOpenIndex = i;
                }
                else if (c == ')')
                {
                    // Закриваюча дужка
                    if (stack.Count == 0)
                    {
                        throw new ArgumentException(UnpairedClosingBracket);
                    }

                    
                    if (i - lastOpenIndex == 2) 
                    {
                        throw new ArgumentException(InvalidContentBetweenBrackets);
                    }

                    stack.Pop();
                }
            }

         
            if (stack.Count != 0)
            {
                throw new ArgumentException(InvalidBracketsClosure);
            }

            return true;
        }
        catch (ArgumentException ex)
        {
          
            Console.WriteLine($"Unhandled error with parentheses: {ex.Message}");
            return false;
        }
    }

}

