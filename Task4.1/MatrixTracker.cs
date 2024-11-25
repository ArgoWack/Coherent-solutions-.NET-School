using static System.Console;

namespace Task41
{
    public class MatrixTracker<T>
    {
        private readonly DiagonalMatrix<T> matrix;
        private readonly Stack<(int Row, int Column, T OldValue)> changeHistory = new Stack<(int, int, T)>();

        public MatrixTracker(DiagonalMatrix<T> matrix)
        {
            this.matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            this.matrix.ElementChanged += OnElementChanged;
        }

        private void OnElementChanged(object sender, ElementChangedEventArgs<T> e)
        {
            // Track changes in history
            changeHistory.Push((e.Row, e.Column, e.OldValue));
            WriteLine($"Element at ({e.Row}, {e.Column}) changed from {e.OldValue} to {e.NewValue}");
        }

        // Undo the last change
        public void Undo()
        {
            if (changeHistory.Count > 0)
            {
                // Unsubscribe from the event to prevent a loop
                matrix.ElementChanged -= OnElementChanged;

                var lastChange = changeHistory.Pop();
                matrix[lastChange.Row, lastChange.Column] = lastChange.OldValue;

                // Resubscribe to the event
                matrix.ElementChanged += OnElementChanged;
            }
            else
            {
                WriteLine("No changes to undo.");
            }
        }
    }
}