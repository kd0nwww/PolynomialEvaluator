using System;
using System.Security;
using System.IO;
using System.Numerics;
using System.Globalization;

class PolynomialEvaluator
{
    public static void Main()
    {
        Console.WriteLine("Polynomial Calculator");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("Type the polynomial you want to calculate...");
        Console.WriteLine("Example: 2x^2 + 3x + 8");
        Console.WriteLine("'q' to exit.");
        Console.WriteLine("---------------------------------------------");

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
            var x = Console.ReadLine();
            int parsedX;

            while (!int.TryParse(x, out parsedX))
            {
                Console.WriteLine("Please enter the integer for x.");
                x = Console.ReadLine();
            }

            var terms = SplitTerms(polyExpression!);
            int[][] coefPowPairs = new int[terms.Count][];

            for (int i = 0; i < terms.Count; i++)
            {
                coefPowPairs[i] = ParseTerm(terms[i]);
            }

            string[] formattedTerms = new string[coefPowPairs.Length];

            for (int i = 0; i < coefPowPairs.Length; i++)
            {
                formattedTerms[i] = HtmlFormatTerm(coefPowPairs[i][0], coefPowPairs[i][1]);
            }

            var result = Calculate(coefPowPairs, parsedX);
            string htmlString = BuildHtmlString(formattedTerms, result, parsedX);

            Console.WriteLine($"Result: {polyExpression} = {result}");
            Console.WriteLine("---------------------------------------------");
            SaveResultToFile(htmlString);
        }
    }

    public static List<string> SplitTerms(string polynomial)
    {
        string sign = "+";
        List<string> termsWithSigns = new List<string>();

        foreach (var s in polynomial.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries))
        {
            string term = s.Trim();
            if ((term.StartsWith("+") || term.StartsWith("-")) && term.Length > 1) termsWithSigns.Add(term);
            else
            {
                if (term == "+" || term == "-") sign = term;
                else termsWithSigns.Add(sign + term);
            }
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

    public static string HtmlFormatTerm(int coef, int power)
    {
        string coefStr;

        if (coef == 1 && power != 0) coefStr = "";
        else if (coef == -1 && power != 0) coefStr = "-";
        else coefStr = coef.ToString();

        if (power == 0) return coefStr;
        else if (power == 1) return coefStr + "x";
        else return $"{coefStr}x<sup>{power}</sup>";
    }

    public static string BuildHtmlString(string[] htmlFormattedTerms, int calcResult, int x)
    {
        if (htmlFormattedTerms.Length == 0) return "";

        string result = htmlFormattedTerms[0];
        if (result.StartsWith("-"))
        {
            result = "- " + result.Substring(1);
        }

        for (int i = 1; i < htmlFormattedTerms.Length; i++)
        {
            if (htmlFormattedTerms[i].StartsWith("-"))
            {
                result += " - " + htmlFormattedTerms[i].Substring(1); 
            }
            else
            {
                result += " + " + htmlFormattedTerms[i];
            }
        }

        return  "\t<div class=\"history-entry\">\n" +
                "\t\t<p class=\"polynomial\">" + result + "</p>\n" +
                "\t\t<p class=\"x-value\">x = " + x.ToString() + "</p>\n" +
                "\t\t<p class=\"result\">Result = " + calcResult.ToString() + "</p>\n" +
                "\t</div>\n";
    }

    public static void SaveResultToFile(string htmlString)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "history.html");

        if (!File.Exists(path))
        {
            Console.WriteLine("History.html not found. Please place history.html in the same folder as the .exe file.");
        }

        string html = File.ReadAllText(path);
        int insertIndex = html.IndexOf("</body>");

        var newHtml = html.Insert(insertIndex, htmlString);
        File.WriteAllText(path, newHtml);
    }
}