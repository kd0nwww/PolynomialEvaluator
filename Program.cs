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
                foreach(var val in ParseTerm(item))
                {
                    Console.WriteLine(val);
                }
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

    public static int[] ParseTerm(string term)
    {
        int coefficient, power;

        if (term.IndexOf("x") == -1)
        {
            coefficient = int.Parse(term);
            power = 0;
        }
        else
        {
            var posX = term.IndexOf("x");
            string coefSubstring = term.Substring(0, posX);
            string powerSubstring = term.Substring(posX + 1);

            if (coefSubstring.Length == 1)
            {
                if (coefSubstring == "+") coefficient = 1;
                else coefficient = -1;
            }
            else coefficient = int.Parse(coefSubstring);

            if (powerSubstring.IndexOf("^") == -1) power = 1;
            else
            {
                power = int.Parse(powerSubstring.Substring(powerSubstring.IndexOf("^") + 1));
            }
        }

        return new int[] { coefficient, power };
    }
}