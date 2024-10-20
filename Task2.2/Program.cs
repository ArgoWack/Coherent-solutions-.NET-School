using static System.Console;
/*
 Task 2.2
 A diagonal matrix is a square matrix in which all elements outside the main diagonal are equal to zero (https://en.wikipedia.org/wiki/Diagonal_matrix).
You need to create and test a class to represent a diagonal matrix containing integers (int).
The class object stores only matrix elements located on the diagonal.  Use one-dimensional array for this.
The class object has a read-only Size property - the size of the matrix (for example, for a 5x5 matrix, the Size property returns the value 5).
The class has a constructor for creating a matrix. A list of parameters (params) is passed to the constructor - these are matrix elements located on the diagonal. If the value of the constructor argument is not correct (for example, equal to null), a zero-size matrix is created (Size=0).
For conveniency the class offers an indexer with two indices i and j. If i is not equal to j, then the indexer returns the value 0 (nothing happens when writing). If the index values are incorrect (out of bounds: less than zero or greater than or equal to Size), nothing happens when writing, and when reading, the value 0 is returned.
(In this situation, of course, you need to throw an exception. However, exceptional situations have not yet been considered during the training)

The matrix class has an instance method Track(), which returns the sum of the matrix elements located on the main diagonal.
Override the Equals() and ToString() methods in the matrix class. Two matrices are considered equal if their sizes and corresponding elements on the diagonal are the same.
Create a diagonal matrix extension method that performs the addition of two diagonal matrices. The result of the method is a new diagonal matrix. If the matrix sizes do not match, the smaller matrix is padded with zeros.
*/

class Program
{
    public class DiagonalMatrix
    {
        private readonly int[] diagonalElements;

        // readonly property to get the size of the matrix
        public int Size { get; }

        // constructor
        public DiagonalMatrix(params int[] elements)
        {
            if (elements == null)
            {
                Size = 0;
                diagonalElements = new int[0];
            }
            else
            {
                Size = elements.Length;
                diagonalElements = new int[Size];
                for (int i = 0; i < Size; i++)
                {
                    diagonalElements[i] = elements[i];
                }
            }
        }

        // indexer
        public int this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size || j != i)
                {
                    return 0; //I don't know which option is preferd given different versions in task description and additional note below
                    // throw new ArgumentOutOfRangeException("Indices are out of bounds.");
                }
                return diagonalElements[i];
            }
            set
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size || i != j)
                {
                    throw new ArgumentOutOfRangeException("Indices are out of bounds.");
                }
                diagonalElements[i] = value;
            }
        }

        // method to sum elements on diagonal (Track)
        public int Track()
        {
            int sum = 0;
            for (int i = 0; i < Size; i++)
            {
                sum += diagonalElements[i];
            }
            return sum;
        }
        public override bool Equals(object obj)
        {
            if (obj is DiagonalMatrix otherMatrix)
            {
                if (Size != otherMatrix.Size)
                {
                    return false;
                }

                for (int i = 0; i < Size; i++)
                {
                    if (diagonalElements[i] != otherMatrix.diagonalElements[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            if (Size == 0) return "[]";

            string result = "[ ";
            for (int i = 0; i < Size; i++)
            {
                result += diagonalElements[i] + " ";
            }
            result += "]";
            return result;
        }

        // Adds matrices treating missing elements as 0's
        public DiagonalMatrix Add(DiagonalMatrix other)
        {
            int maxSize = Math.Max(this.Size, other.Size);
            int[] resultElements = new int[maxSize];

            for (int i = 0; i < maxSize; i++)
            {
                int value1 = i < this.Size ? this[i, i] : 0;
                int value2 = i < other.Size ? other[i, i] : 0;
                resultElements[i] = value1 + value2;
            }

            return new DiagonalMatrix(resultElements);
        }
    }

    public static void Main()
    {
        // creating and testing DiagonalMatrix
        DiagonalMatrix matrix1 = new DiagonalMatrix(1, 2, 3);
        DiagonalMatrix matrix2 = new DiagonalMatrix(2, 2, 6);

        // testing properties and methods
        WriteLine($"Matrix1: {matrix1}");
        WriteLine($"Matrix2: {matrix2}");

        // indexer usage
        WriteLine($"Matrix1[1,1]: {matrix1[1, 1]}");
        WriteLine($"Matrix1[1,2]: {matrix1[1, 2]}");

        // testing the Track method
        WriteLine($"Track of Matrix1: {matrix1.Track()}");
        WriteLine($"Track of Matrix2: {matrix2.Track()}");

        // testing Equals method
        WriteLine($"Matrix1 equals Matrix2? {matrix1.Equals(matrix2)}");

        // testing ToString method
        WriteLine($"Matrix1 as string: {matrix1}");

        // adding matrices using the Add method integrated into DiagonalMatrix
        DiagonalMatrix sumMatrix = matrix1.Add(matrix2);
        WriteLine($"Sum of Matrix1 and Matrix2: {sumMatrix}");

        // test with incorrect indices
        WriteLine($"Matrix1[-1,1]: {matrix1[-1, 1]}");
        WriteLine($"Matrix1[5,5]: {matrix1[5, 5]}");
    }
}