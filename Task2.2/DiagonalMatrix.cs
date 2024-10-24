using System;

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
                // simplified to using Array.Copy instead of loop according to review
                Array.Copy(elements, diagonalElements, Size);
            }
        }
    //Added separate method for validation
    private bool IsProperSize(int i, int j)
    {
        if (i < 0 || i >= Size || j < 0 || j >= Size || j != i)
        {
            return false;
        }
        return true;
    }
    // indexer
    public int this[int i, int j]
        {
            get
            {
                if (!IsProperSize(i, j))
                {
                    return 0;
                }
                return diagonalElements[i];
            }
            set
            {
                if (!IsProperSize(i, j))
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

        // Adds matrices treating missing elements as 0's
        public DiagonalMatrix Add(DiagonalMatrix other)
        {
            if (other == null)
            {
                //in case of other martix being null it returns copy of current matrix resulting in no changes
                return new DiagonalMatrix((int[])this.diagonalElements.Clone());
            }

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
