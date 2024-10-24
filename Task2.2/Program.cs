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

        // testing ToFString method
        WriteLine($"Matrix1 as string: {matrix1.ToFormattedString()}");

        // adding matrices using the Add method integrated into DiagonalMatrix
        DiagonalMatrix sumMatrix = matrix1.Add(matrix2);
        WriteLine($"Sum of Matrix1 and Matrix2: {sumMatrix}");

        // test with incorrect indices
        WriteLine($"Matrix1[-1,1]: {matrix1[-1, 1]}");
        WriteLine($"Matrix1[5,5]: {matrix1[5, 5]}");
    }
}