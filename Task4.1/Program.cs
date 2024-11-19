using static System.Console;

/*
Task 4.1.
This task is based on the diagonal matrix task. By classical definition, a numeric diagonal matrix is a square matrix in which all elements outside the main diagonal are equal to zero. In the same assignment, you need to create a generic class for a diagon al matrix with elements of any type T (elements off the main diagonal are equal to the default value for T ).
To do:
1. The class object stores only matrix elements located on the diagonal. A one-dimensional array is used for this.
2. The class has a constructor for creating a matrix. The size of the matrix is passed to the constructor (for example, 5 for a 5x5 matrix). If the argument is negative, an ArgumentException is thrown.
3. The class object has a read
- only Size property
- the size of the matrix (for example, for a 5x5 matrix, the Size property returns 5).
4. The class offers an indexer with two indices i and j. If index values are less than zero or greater than or equal to the matrix size, an IndexOutOfRangeException is thrown. If i is not equal to j : when reading, the default value for type T is returned, and when writing, nothing happens.
5. The matrix class contains an ElementChanged event, which occurs after a matrix element is changed, and only if the new value of the element is different from the old value. The element's indices, the old value of the element, and the new value of the element are transmitted to the event as additional information. 
Attention: use standard practices for working with events!
6. Create a diagonal matrix extension method that performs the addition of two diagonal matrices. One of the method's parameters must be an instance of a delegate that describes how to perform the addition of two elements of type T (this is a function with two parameters). Test the extension method.
7. Implement a MatrixTracker class that takes a diagonal matrix as a constructor parameter, subscribes to its ElementChanged event, and has a public Undo() method. When this method is called, the last element change made in the matrix is rolled back (i.e., undone).
*/

namespace Task41
{
    class Program
    {
        static void Task41 (string[] args)
        {
            // Step 1: Create two DiagonalMatrix<int> objects
            WriteLine("Step 1: Creating two 3x3 diagonal matrices.");
            DiagonalMatrix<int> matrix1 = new DiagonalMatrix<int>(3);
            DiagonalMatrix<int> matrix2 = new DiagonalMatrix<int>(3);

            // Step 2: Set diagonal elements for both matrices
            WriteLine("Step 2: Setting values for matrix1 (1, 2, 3) and matrix2 (4, 5, 6).");
            matrix1[0, 0] = 1;
            matrix1[1, 1] = 2;
            matrix1[2, 2] = 3;

            matrix2[0, 0] = 4;
            matrix2[1, 1] = 5;
            matrix2[2, 2] = 6;

            // Display initial matrices
            WriteLine("\nInitial matrix1:");
            DisplayMatrix(matrix1);

            WriteLine("Initial matrix2:");
            DisplayMatrix(matrix2);

            // Step 3: Track changes in matrix1 using MatrixTracker
            WriteLine("\nStep 3: Tracking changes in matrix1.");
            MatrixTracker<int> tracker = new MatrixTracker<int>(matrix1);

            // Step 4: Change an element in matrix1
            WriteLine("Step 4: Changing matrix1[1, 1] from 2 to 10.");
            matrix1[1, 1] = 10;

            // Display matrix1 after the change
            WriteLine("Matrix1 after change:");
            DisplayMatrix(matrix1);

            // Step 5: Undo the change using MatrixTracker
            WriteLine("\nStep 5: Undoing the last change in matrix1.");
            tracker.Undo();

            // Display matrix1 after undo
            WriteLine("Matrix1 after undo:");
            DisplayMatrix(matrix1);

            // Step 6: Add matrix1 and matrix2 using the extension method
            WriteLine("\nStep 6: Adding matrix1 and matrix2 using the extension method.");
            DiagonalMatrix<int> sumMatrix = matrix1.Add(matrix2, (x, y) => x + y);

            // Display the resulting sumMatrix
            WriteLine("Resulting sumMatrix:");
            DisplayMatrix(sumMatrix);

        }

        // helper method for display
        static void DisplayMatrix<T>(DiagonalMatrix<T> matrix)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    Write($"{matrix[i, j]} ");
                }
                WriteLine();
            }
            WriteLine();
        }
    }
}