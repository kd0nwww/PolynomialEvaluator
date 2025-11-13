using System; 

class PolynomialEvaluator
{
    public static void Main()
    {
        string userInput;
        Console.WriteLine("Polynomial Calculator");
        Console.WriteLine("Type the polynomial you want to calculate.");
        Console.WriteLine("Example: 2x^2 + 3x + 8");
        Console.WriteLine("'q' to exit.");

        while (true)
        {
            userInput = Console.ReadLine();
                
            if (userInput == "q") break;
            if (userInput == "") Console.WriteLine("Please enter the expression");
        }
    }
}