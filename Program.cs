using System; 

class PolynomialEvaluator
{
    public static void Main()
    {
        string polyExpression;
        Console.WriteLine("Polynomial Calculator");
        Console.WriteLine("Type the polynomial you want to calculate.");
        Console.WriteLine("Example: 2x^2 + 3x + 8");
        Console.WriteLine("'q' to exit.");

        while (true)
        {
            polyExpression = Console.ReadLine();
                
            if (polyExpression == "q") break;
            if (polyExpression == "") Console.WriteLine("Please enter the expression");

            foreach(var item in SplitTerms(polyExpression))
            {
                Console.WriteLine(item);
            }
        }
    }

    public static List<string> SplitTerms(string polynomial)
    {
        string sign = "+";
        List<string> termsWithSigns = new List<string>();

        foreach(var s in polynomial.Trim().Split(" "))
        {
            if (s == "") continue;
            if (s == "+" || s == "-") sign = s;
            else termsWithSigns.Add(sign + s);
        }
        return termsWithSigns;
    }
}