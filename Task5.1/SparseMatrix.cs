using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task51
{
    public class SparseMatrix : IEnumerable<long>
    {
        // Dimensions of the matrix
        public int Rows { get; }
        public int Columns { get; }

        // Internal storage for non-zero elements
        private readonly Dictionary<(int, int), long> nonZeroElements;

        // Constructor
        public SparseMatrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException("Rows and columns must be greater than zero.");

            Rows = rows;
            Columns = columns;
            nonZeroElements = new Dictionary<(int, int), long>();
        }

        private void ValidateIndex(int i, int j)
        {
            if (i < 0 || i >= Rows || j < 0 || j >= Columns)
                throw new IndexOutOfRangeException("Index out of bounds.");
        }

        // Indexer
        public long this[int i, int j]
        {
            get
            {
                ValidateIndex(i, j);
                return nonZeroElements.TryGetValue((i, j), out long value) ? value : 0;
            }
            set
            {
                ValidateIndex(i, j);
                if (value != 0)
                {
                    nonZeroElements[(i, j)] = value;
                }
                else
                {
                    nonZeroElements.Remove((i, j));
                }
            }
        }

        // ToString method override for debugging
        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    result.Append(this[i, j] + " ");
                }
                result.AppendLine();
            }
            return result.ToString();
        }

        // IEnumerable<long> implementation
        public IEnumerator<long> GetEnumerator()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // GetNonzeroElements method
        public IEnumerable<(int, int, long)> GetNonzeroElements()
        {
            return nonZeroElements
                .OrderBy(element => element.Key.Item2) // Order by column
                .ThenBy(element => element.Key.Item1)  // Then by row
                .Select(element => (element.Key.Item1, element.Key.Item2, element.Value));
        }

        // GetCount method
        public int GetCount(long x) =>
            x == 0 ? Rows * Columns - nonZeroElements.Count
                   : nonZeroElements.Values.Count(value => value == x);
    }
}
