using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task42
{
    public sealed class RationalNumber : IComparable<RationalNumber>
    {
        // Properties
        public int Numerator { get; }
        public int Denominator { get; }

        // Constructor
        public RationalNumber(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be zero.");

            // Reduce the fraction
            int gcd = GreatestCommonDivisor(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;

            // Ensure the denominator is always positive
            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        // GreatestCommonDivisor using Euclidean algorithm
        private static int GreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Equals method override
        public override bool Equals(object obj)
        {
            if (obj is RationalNumber other)
            {
                return Numerator == other.Numerator && Denominator == other.Denominator;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        // ToString method override
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        // IComparable<T> implementation
        public int CompareTo(RationalNumber other)
        {
            if (other == null) return 1;

            // Cross multiplication to compare without converting to floating point
            return (Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);
        }

        // Arithmetic operators
        public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber(
                r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator,
                r1.Denominator * r2.Denominator
            );
        }

        public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber(
                r1.Numerator * r2.Denominator - r2.Numerator * r1.Denominator,
                r1.Denominator * r2.Denominator
            );
        }

        public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber(
                r1.Numerator * r2.Numerator,
                r1.Denominator * r2.Denominator
            );
        }

        public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        {
            if (r2.Numerator == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            return new RationalNumber(
                r1.Numerator * r2.Denominator,
                r1.Denominator * r2.Numerator
            );
        }

        // Explicit cast to double
        public static explicit operator double(RationalNumber r)
        {
            return (double)r.Numerator / r.Denominator;
        }

        // Implicit cast from int
        public static implicit operator RationalNumber(int value)
        {
            return new RationalNumber(value, 1);
        }
    }
}