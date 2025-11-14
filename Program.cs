using System;
using System.Security;

class PolynomialEvaluator
{
    public static void Main()
    {
        Console.WriteLine("Polynomial Calculator");
        Console.WriteLine("Type the polynomial you want to calculate...");
        Console.WriteLine("Example: 2x^2 + 3x + 8");
        Console.WriteLine("'q' to exit.");

        while (true)
        {
            var polyExpression = Console.ReadLine();
                
            if (polyExpression == "q") break;
            if (polyExpression == "")
            {
                Console.WriteLine("Please enter the expression");
                continue;
            }

            Console.WriteLine("Provide x value for the expression...");
            var xValue = Console.ReadLine();

            if (!int.TryParse(xValue, out var parsedX))
            {
                Console.WriteLine("Please enter the integer for x.");
                continue;
            }

            var terms = SplitTerms(polyExpression);
            int[][] coefPowPairs = new int[terms.Count][];

            for (int i = 0; i < terms.Count; i++)
            {
                coefPowPairs[i] = ParseTerm(terms[i]);
            }

            var result = Calculate(coefPowPairs, parsedX);
            Console.WriteLine(polyExpression + " = " + result);
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

    public static int Calculate(int[][] coefPowerPairs, int x)
    {
        int sum = 0;

        foreach(var coefPowerPair in coefPowerPairs)
        {
            int coefficient = coefPowerPair[0];
            int power = coefPowerPair[1];

            sum += PowInt(x, power) * coefficient;  
        }

        return sum;
    }

    public static int PowInt(int x, int power)
    {
        int result = 1;

        for (int i = 0; i < power; i ++)
        {
            result *= x;
        }

        return result;
    }

}