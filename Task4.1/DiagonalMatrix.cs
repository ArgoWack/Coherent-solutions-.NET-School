using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task41
{
    public class DiagonalMatrix<T>
    {
        private readonly T[] diagonalElements;
        private Stack<(int Index, T OldValue)> changeHistory = new Stack<(int, T)>();
        public int Size { get; }

        // constructor
        public DiagonalMatrix(int size)
        {
            if (size < 0)
                throw new ArgumentException("Size cannot be negative.");

            Size = size;
            diagonalElements = new T[size];
        }
        private bool IsProperIndex(int i, int j)
        {
            if (i < 0 || i >= Size || j < 0 || j >= Size)
            {
                return false;
            }
            return true;
        }

        // indexer
        public T this[int i, int j]
        {
            get
            {
                if (!IsProperIndex(i, j))
                    throw new IndexOutOfRangeException("Index out of range.");

                if (i != j)
                    return default;

                return diagonalElements[i];
            }
            set
            {
                if (!IsProperIndex(i, j))
                    throw new IndexOutOfRangeException("Index out of range.");

                if (i == j)
                {
                    T oldValue = diagonalElements[i];
                    if (!EqualityComparer<T>.Default.Equals(oldValue, value))
                    {
                        diagonalElements[i] = value;
                        ElementChanged?.Invoke(this, new ElementChangedEventArgs<T>(i, j, oldValue, value));
                        changeHistory.Push((i, oldValue));
                    }
                }
            }
        }

        // ElementChanged event
        public event EventHandler<ElementChangedEventArgs<T>> ElementChanged;

        // Undo Method
        public void Undo()
        {
            if (changeHistory.Count > 0)
            {
                var lastChange = changeHistory.Pop();
                diagonalElements[lastChange.Index] = lastChange.OldValue;
            }
        }
    }
}