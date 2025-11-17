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

            string[] formattedTerms = new string[coefPowPairs.Length];

            for (int i = 0; i < coefPowPairs.Length; i++)
            {
                formattedTerms[i] = HtmlFormatTerm(coefPowPairs[i][0], coefPowPairs[i][1]);
            }

            var result = Calculate(coefPowPairs, parsedX);
            string htmlString = BuildHtmlString(formattedTerms, result, parsedX);

            
            Console.WriteLine(polyExpression + " = " + result);
            SaveResultToFile(htmlString, parsedX, result);
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

    public static string HtmlFormatTerm(int coef, int power)
    {
        if (power == 0)
        {
            return coef.ToString();
        }

        else if (power == 1)
        {
            return coef.ToString() + "x";
        }

        else
        {
            return $"{coef}x<sup>{power}</sup>";
        }
    }

    public static string BuildHtmlString(string[] htmlFormattedTerms, int calcResult, int x)
    {

        string result = "";
        if (htmlFormattedTerms[0][0] == '-')
        {
            result += "- " + htmlFormattedTerms[0].Substring(0);
        }
        else
        {
            result += htmlFormattedTerms[0];
        }

        for (int i = 1; i < htmlFormattedTerms.Length; i ++)
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

        return "\t<div class=\"history-entry\">\n" +
                "\t\t<p class=\"polynomial\">" + result +"</p>\n" +
                "\t\t<p class=\"x-value\">x = " + x.ToString() + "</p>\n" +
                "\t\t<p class=\"result\">Result = " + calcResult.ToString() + "</p>\n" +
                "\t</div>\n";
    }

    public static void SaveResultToFile(string htmlString, int x, int result)
    {
        string path = "C:/Users/user2/Desktop/C#/PolynomialEvaluator/history.html";
        string html = File.ReadAllText(path);
        int insertIndex = html.IndexOf("</body>");

        string newHtml =  html.Substring(0, insertIndex)  + htmlString + html.Substring(insertIndex);
        File.WriteAllText(path, newHtml);
    }
}