using System;

    public static class DiagonalMatrixExtensions
    {
        public static string ToFormattedString(this DiagonalMatrix matrix)
        {
            if (matrix.Size == 0) return "[]";

            string result = "[ ";
            for (int i = 0; i < matrix.Size; i++)
            {
                result += matrix[i, i] + " ";
            }
            result += "]";
            return result;
        }
}
