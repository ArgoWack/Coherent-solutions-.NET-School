
using System.Text.RegularExpressions;

namespace Task52
{
    public class ISBN
    {
        private static readonly Regex isbnFormat = new Regex(@"^\d{3}-\d-\d{2}-\d{6}-\d$|^\d{13}$");
        private readonly string normalizedIsbn;

        public ISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn) || !isbnFormat.IsMatch(isbn))
                throw new ArgumentException("Invalid ISBN format.");

            normalizedIsbn = NormalizeIsbn(isbn);
        }

        private static string NormalizeIsbn(string isbn)
        {
            return isbn.Replace("-", "");
        }

        public override bool Equals(object obj)
        {
            return obj is ISBN other && normalizedIsbn == other.normalizedIsbn;
        }

        public override int GetHashCode()
        {
            return normalizedIsbn.GetHashCode();
        }

        public override string ToString()
        {
            return normalizedIsbn;
        }
    }
}
