using System.Text.RegularExpressions;
namespace Task7
{
    public class ISBN
    {
        private static readonly Regex isbnFormat = new Regex(@"^\d{3}-\d-\d{2}-\d{6}-\d$|^\d{13}$");
        private string normalizedIsbn;
        public ISBN() { }
        public ISBN(string isbn)
        {
            normalizedIsbn = NormalizeIsbn(isbn);
            Validate();
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
        public void Validate()
        {
            ArgumentException.ThrowIfNullOrEmpty(normalizedIsbn);

            if (!isbnFormat.IsMatch(normalizedIsbn))
                throw new ArgumentException($"Invalid ISBN format: {normalizedIsbn}. Ensure it matches XXX-X-XX-XXXXXX-X or a 13-digit number.");
        }
    }
}