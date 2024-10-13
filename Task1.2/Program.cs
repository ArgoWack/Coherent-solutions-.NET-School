using static System.Console;
/*
Task  1.2
Let 10-character ISBN is an unique digital code to identify a book, with form: . Digit  is a control one, it is calculated according the condition, that expression 

(the sum of the products of digits by the weight of their positions) has to be a multiple of 11.
Create an application that prompts the user for a string of 9 digit characters (these are the first nine digits of the ISBN), calculates the check digit, and outputs the resulting ISBN. Do not check the correctness of the user's input - assume that the user does not make errors when entering.
Note 1. Сheck «digit» can be equal to 10. In this case use symbol X to denote it.
Note 2. You can convert any a value to string using a.ToString().
*/

class Program
{
    public static void Main()
    {
        // expected 9 digits of the ISBN
        WriteLine("Please enter the first 9 digits of the ISBN:");
        string isbnFirstNineDigits = ReadLine();

        char checkDigit = CalculateCheckDigit(isbnFirstNineDigits);

        // outputs full ISBN
        string fullIsbn = isbnFirstNineDigits + checkDigit;
        WriteLine("The full 10-character ISBN is: " + fullIsbn);
    }

    public static char CalculateCheckDigit(string isbnFirstNineDigits)
    {
        int sum = 0;

        // loops through the first 9 digits, multiplying each digit by its position (1-9)
        for (int i = 0; i < 9; i++)
        {
            int digit = isbnFirstNineDigits[i] - '0'; // converts char to int
            int weight = i + 1;
            sum += digit * weight;
        }

        //  remainder of the sum after divide by 11
        int remainder = sum % 11;

        // if remainder is 10, returns 'X'. Otherwise, returns the remainder as a digit
        return remainder == 10 ? 'X' : remainder.ToString()[0];
    }
}
