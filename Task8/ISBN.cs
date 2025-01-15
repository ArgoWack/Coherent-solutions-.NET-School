namespace Task8
{
    public class ISBN
    {
        private string normalizedIsbn;
        public ISBN() { }
        public ISBN(string isbn)
        {
            normalizedIsbn = NormalizeIsbn(isbn);
        }
        private static string NormalizeIsbn(string isbn)
        {
            return isbn?.Replace("-", "");
        }
        public override bool Equals(object obj)
        {
            return obj is ISBN other && normalizedIsbn == other.normalizedIsbn;
        }
        public override int GetHashCode()
        {
            return normalizedIsbn?.GetHashCode() ?? 0;
        }
        public override string ToString()
        {
            return normalizedIsbn;
        }
        public string Value
        {
            get => normalizedIsbn;
            set
            {
                normalizedIsbn = NormalizeIsbn(value);
            }
        }
    }
}