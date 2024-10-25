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

    // extension method to add two diagonal matrices
    public static DiagonalMatrix Add(this DiagonalMatrix matrix1, DiagonalMatrix matrix2)
    {
        if (matrix2 == null)
        {
            return new DiagonalMatrix(matrix1.GetDiagonalElements());
        }

        int maxSize = Math.Max(matrix1.Size, matrix2.Size);
        int[] resultElements = new int[maxSize];

        for (int i = 0; i < maxSize; i++)
        {
            int value1 = i < matrix1.Size ? matrix1[i, i] : 0;
            int value2 = i < matrix2.Size ? matrix2[i, i] : 0;
            resultElements[i] = value1 + value2;
        }

        return new DiagonalMatrix(resultElements);
    }
}