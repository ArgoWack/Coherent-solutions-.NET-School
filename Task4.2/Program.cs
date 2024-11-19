using static System.Console;

/*
Task 4.2.
Create a sealed, immutable class to represent a rational number, that is, a number of the form n/m, where n is an integer and m is a natural number.
To do:
1. A rational number must be stored as an irreducible fraction. For example, the number 2/6 should be stored as Numerator = 1, Denominator = 3.
2. Define a constructor in the class that takes the Numerator and Denominator of a rational number as parameters. If Denominator = 0, an exception is thrown.
3. Override the Equals() and ToString() methods in the class.
4. Implement the IComparable<T> interface in the class.
5. Redefine the arithmetic operators +, - , * and / in the class.
6. Override in the class the method for explicitly casting a rational number to a double and the method for implicitly cast ing an int value to a rational number.
*/

namespace Task42
{
    class Program
    {
        static void Task42()
        {
            // Creating rational numbers
            RationalNumber r1 = new RationalNumber(2, 6);
            RationalNumber r2 = new RationalNumber(3, 4);
            RationalNumber r3 = new RationalNumber(3, 4);

            // Displaying the rational numbers
            WriteLine($"Rational number r1: {r1}");
            WriteLine($"Rational number r2: {r2}");
            WriteLine($"Rational number r3: {r3}");

            // Arithmetic operations
            RationalNumber sum = r1 + r2;
            RationalNumber difference = r1 - r2;
            RationalNumber product = r1 * r2;
            RationalNumber quotient = r1 / r2;

            WriteLine($"\nr1 + r2 = {sum}");
            WriteLine($"r1 - r2 = {difference}");
            WriteLine($"r1 * r2 = {product}");
            WriteLine($"r1 / r2 = {quotient}");

            // Comparison
            WriteLine($"\nr1 equals r2? {r1.Equals(r2)}");
            WriteLine($"r2 equals r3? {r2.Equals(r3)}");
            WriteLine($"r1 compared to r2: {r1.CompareTo(r2)}");

            // Casting to double
            double r1AsDouble = (double)r1;
            WriteLine($"\nExplicit cast of r1 to double: {r1AsDouble}");

            // Implicit cast from int
            RationalNumber r4 = 5;
            WriteLine($"\nImplicit cast from int 5 to rational: {r4}");
        }
    }
}