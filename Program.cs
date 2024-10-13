using static System.Console;
/*
Task 1.1
For numbers in duodecimal numerical system, the symbols 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, A (ten) and B (eleven) are used. When starting, the application asks the user to input two integers a and b (assume that the user enters integers without errors). Then the application displays all integers in the range from a (inclusive) to b (inclusive), which in their duodecimal representation have exactly two symbols A. Develop a console application that implements the specified functionality.
Note 1. In order to transform string s to int use method int.Parse(s).
Note 2. Output required numbers in decimal (not duodecimal) numerical system. 
*/

// examples A11A, AAB, 22AA.

class Program
{
    public static void Main()
    {
        WriteLine("Please input first duodecimal number");
        string firstDuodecimalNumber = ReadLine();

        WriteLine("Please input second duodecimal number");
        string secondDuodecimalNumber = ReadLine();

        int firstNumber = ConvertToDecimal(firstDuodecimalNumber);
        int secondNumber = ConvertToDecimal(secondDuodecimalNumber);

        int start = Math.Min(firstNumber, secondNumber);
        int end = Math.Max(firstNumber, secondNumber);

        for (int i = start; i <= end; i++)
        {
            string duodecimalRepresentation = ConvertToDuodecimal(i);

            int aCount = CountA(duodecimalRepresentation);

            if (aCount == 2)
            {
                WriteLine(i);
            }
        }
    }

    public static int ConvertToDecimal(string duodecimal)
    {
        int result = 0;
        int baseValue = 12;

        foreach (char c in duodecimal)
        {
            result *= baseValue; // Shifts previous digits to the left
            if (c >= '0' && c <= '9') // Handles digits 0-9
            {
                result += c - '0';
            }
            else if (c == 'A') // A = 10
            {
                result += 10;
            }
            else if (c == 'B') // B = 11
            {
                result += 11;
            }
        }

        return result;
    }
    public static string ConvertToDuodecimal(int number)
    {
        if (number == 0)
        {
            return "0";
        }

        string result = "";
        while (number > 0)
        {
            int remainder = number % 12;
            result = (remainder == 10 ? "A" : remainder == 11 ? "B" : remainder.ToString()) + result;
            number /= 12;
        }
        return result;
    }

    public static int CountA(string duodecimal)
    {
        int count = 0;
        foreach (char c in duodecimal)
        {
            if (c == 'A')
            {
                count++;
            }
        }
        return count;
    }
}