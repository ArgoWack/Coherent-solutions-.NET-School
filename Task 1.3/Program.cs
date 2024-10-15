using static System.Console;
/*
Task 1.3
When starting, the application asks the user for the number of elements in the integer array, and then in a loop - the elements themselves (of type int). After entering the elements, the application outputs the original array. Then create a new array from the original array according to the principle “each value – only once”, and output it to console. For example, for the array [1, 1, 3, 4, 2, 2, 6, 7, 1], the array [1, 3, 4, 2, 6, 7] is created. Develop a console application that implements the specified functionality.
Note 1. The correctness of the array length may not be controlled.
Note 2. Do not use the standard collections and LINQ!
*/

class Program
{
    public static void Main()
    {
        WriteLine("Please enter the number of elements in the array:");
        int numberOfElements = int.Parse(ReadLine());

        int[] originalArray = new int[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            Write($"Enter element {i + 1}: ");
            originalArray[i] = int.Parse(ReadLine());
        }

        WriteLine("Original array:");
        PrintArray(originalArray);

        int[] uniqueArray = CreateUniqueArray(originalArray);

        WriteLine("Array with unique elements:");
        PrintArray(uniqueArray);
    }

    public static void PrintArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Write(array[i] + " ");
        }
        WriteLine();
    }
    public static int[] CreateUniqueArray(int[] originalArray)
    {
        int[] uniqueArray = new int[originalArray.Length];
        int uniqueCount = 0;

        for (int i = 0; i < originalArray.Length; i++)
        {
            bool isDuplicate = false;

            for (int j = 0; j < uniqueCount; j++)
            {
                if (originalArray[i] == uniqueArray[j])
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (!isDuplicate)
            {
                uniqueArray[uniqueCount] = originalArray[i];
                uniqueCount++;
            }
        }

        int[] resultArray = new int[uniqueCount];
        for (int i = 0; i < uniqueCount; i++)
        {
            resultArray[i] = uniqueArray[i];
        }

        return resultArray;
    }
}