using static System.Console;

namespace Task41
{
    public class MatrixTracker<T>
    {
        private readonly DiagonalMatrix<T> matrix;

        public MatrixTracker(DiagonalMatrix<T> matrix)
        {
            this.matrix = matrix;
            this.matrix.ElementChanged += OnElementChanged;
        }

        private void OnElementChanged(object sender, ElementChangedEventArgs<T> e)
        {
            WriteLine($"Element at ({e.Row}, {e.Column}) changed from {e.OldValue} to {e.NewValue}");
        }

        public void Undo()
        {
            matrix.Undo();
        }
    }
}