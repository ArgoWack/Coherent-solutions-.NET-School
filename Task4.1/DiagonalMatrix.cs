
namespace Task41
{
    public class DiagonalMatrix<T>
    {
        private readonly T[] diagonalElements;

        public int Size { get; }

        // Constructor
        public DiagonalMatrix(int size)
        {
            if (size < 0)
                throw new ArgumentException("Size cannot be negative.");

            Size = size;
            diagonalElements = new T[size];
        }

        // ElementChanged event
        public event EventHandler<ElementChangedEventArgs<T>> ElementChanged;

        private bool IsProperIndex(int i, int j) => !(i < 0 || i >= Size || j < 0 || j >= Size);

        // Indexer
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

                        // Trigger the ElementChanged event
                        ElementChanged?.Invoke(this, new ElementChangedEventArgs<T>(i, j, oldValue, value));
                    }
                }
            }
        }
    }
}