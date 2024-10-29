using System.Linq.Expressions;
using static System.Console;
/*
Task  1.2
Let 10-character ISBN is an unique digital code to identify a book, with form: . Digit  is a control one, it is calculated according the condition, that expression 

(the sum of the products of digits by the weight of their positions) has to be a multiple of 11.
Create an application that prompts the user for a string of 9 digit characters (these are the first nine digits of the ISBN), calculates the check digit, and outputs the resulting ISBN. Do not check the correctness of the user's input - assume that the user does not make errors when entering.
Note 1. Сheck «digit» can be equal to 10. In this case use symbol X to denote it.
Note 2. You can convert any a value to string using a.ToString().
*/
namespace Task12
{
    class Program
    {
        //example input 030640615, output 2
        public static void RunTask12()
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
                int digit = isbnFirstNineDigits[i] - '0'; // convert char to int
                int weight = 10 - i; // position weights start from 10 and go down to 2
                sum += digit * weight;
            }
            //  11 minus remainder of the sum after divide by 11 to get reverse reminder
            int reverseRemainder = 11 - sum % 11;

            switch (reverseRemainder)
            {
                case 11:
                    //if remainder is 11, returns '0'.
                    return '0';
                case 10:
                    //if remainder is 10, returns 'X'.
                    return 'X';
                default:
                    //when remainder is between 0-9, return 0-9 accordingly
                    return reverseRemainder.ToString()[0];
            }
        }
    }
}
