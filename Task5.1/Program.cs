using static System.Console;

/*
Task 5.1
Sparse numeric matrices are matrices in which only a relatively small portion of the elements are non - zero(https://en.wikipedia.org/wiki/Sparse_matrix ).
For example, if in a matrix of size 100x100 only the corner elements are non - zero (there are 4 of them), then we can definitely call such a matrix sparse. Obviously, in order to save memory, it is advisable not to store zero elements of a sparse matrix. You need to create and test a class to represent a sparse numeric matrix.
To do:
1. Create a SparseMatrix class to represent a sparse numeric matrix (matrix elements are of type long). A prerequisite is that the class must have a constructor that allows you to set the dimensions of the matrix (the number of rows and the number of columns, strictly greater than zero), and an indexer with two indices for accessing matrix elements. 
You can use any standard collection of your choice as internal storage. It is desirable to ensure acceptable performance when accessing matrix elements and reasonable memory consumption. For ease of debugging, override the ToString() method in the matrix class.
2. Implement the IEnumerable <long> interface in the SparseMatrix class. All elements of the matrix must be returned, including zero ones (a row - by - row traversal is done).
3. Describe the GetNonzeroElements() method in the SparseMatrix class. The return type is IEnumerable<(int, int, long)>. The method returns a set of tuples of the form (index_i, index_j, value) for all non - null elements.
Indexes must be orderedby column, then by row - that is, first the non - zero elements from the first column, then the non - zero elements from thesecond column, etc.
4. Provide the SparseMatrix class with the GetCount(x) method. It should return how many times element x appears in the matrix. When implementing the GetCount(x) method, be aware that the argument x can be 0.
*/

namespace Task51
{
    class Program
    {
        static void Task51()
        {
            // Creating a sparse matrix of size 5x5
            SparseMatrix matrix = new SparseMatrix(5, 5);

            WriteLine("Added element for [0,0] with value 1");
            WriteLine("Added element for [4,4] with value 2");
            WriteLine("Added element for [2,3] with value 5");

            // Setting some non-zero elements
            matrix[0, 0] = 1;
            matrix[4, 4] = 2;
            matrix[2, 3] = 5;

            // Displaying the matrix
            WriteLine("\nSparse Matrix:");
            WriteLine(matrix);

            // Enumerating all elements (including zero ones)
            WriteLine("All elements (row-by-row into single row):");
            foreach (long element in matrix)
            {
                Write(element + " ");
            }
            WriteLine();

            // Getting non-zero elements
            WriteLine("\nNon-zero elements (ordered by column, then row):");
            foreach (var (i, j, value) in matrix.GetNonzeroElements())
            {
                WriteLine($"({i}, {j}) = {value}");
            }

            // Counting occurrences of elements
            WriteLine($"\nCount of element 0: {matrix.GetCount(0)}");
            WriteLine($"Count of element 1: {matrix.GetCount(1)}");
            WriteLine($"Count of element 5: {matrix.GetCount(5)}");
        }
    }
}