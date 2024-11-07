using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task52
{
    public class Catalog
    {
        private readonly Dictionary<string, Book> books = new Dictionary<string, Book>();
        private static readonly Regex isbnFormat = new Regex(@"^\d{3}-\d-\d{2}-\d{6}-\d$|^\d{13}$");

        // Adds a book to the catalog
        public void AddBook(string isbn, Book book)
        {
            if (string.IsNullOrWhiteSpace(isbn) || !isbnFormat.IsMatch(isbn))
                throw new ArgumentException("Invalid ISBN format.");

            string normalizedIsbn = NormalizeIsbn(isbn);
            books[normalizedIsbn] = book;
        }

        // Retrieves a book by ISBN
        public Book GetBook(string isbn)
        {
            string normalizedIsbn = NormalizeIsbn(isbn);
            return books.TryGetValue(normalizedIsbn, out Book book) ? book : null;
        }

        // Helper method to normalize ISBN by removing hyphens
        private static string NormalizeIsbn(string isbn)
        {
            return isbn.Replace("-", "");
        }

        // a) Get a set of book titles, sorted alphabetically
        public IEnumerable<string> GetSortedTitles()
        {
            return books.Values.Select(book => book.Title).OrderBy(title => title);
        }

        // b) Retrieve a set of books by the specified author name, sorted by publication date
        public IEnumerable<Book> GetBooksByAuthor(string authorName)
        {
            return books.Values
                .Where(book => book.Authors.Contains(authorName))
                .OrderBy(book => book.PublicationDate);
        }

        // c) Get a set of tuples: "author’s name – the number of his books in the catalog"
        public IEnumerable<(string Author, int BookCount)> GetAuthorBookCounts()
        {
            return books.Values
                .SelectMany(book => book.Authors)
                .GroupBy(author => author)
                .Select(group => (Author: group.Key, BookCount: group.Count()));
        }
    }
}
