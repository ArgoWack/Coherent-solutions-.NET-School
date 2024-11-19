
namespace Task41
{
    public static class DiagonalMatrixExtensions
    {
        public static DiagonalMatrix<T> Add<T>(this DiagonalMatrix<T> matrix1,DiagonalMatrix<T> matrix2,Func<T, T, T> addFunction)
        {
            int maxSize = Math.Max(matrix1.Size, matrix2.Size);
            DiagonalMatrix<T> result = new DiagonalMatrix<T>(maxSize);

            for (int i = 0; i < maxSize; i++)
            {
                T value1 = i < matrix1.Size ? matrix1[i, i] : default;
                T value2 = i < matrix2.Size ? matrix2[i, i] : default;
                result[i, i] = addFunction(value1, value2);
            }
            return result;
        }
    }
}