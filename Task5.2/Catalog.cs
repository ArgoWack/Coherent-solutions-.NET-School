using System.Text.RegularExpressions;

namespace Task52
{
    public class Catalog
    {
        private readonly Dictionary<ISBN, Book> books = new Dictionary<ISBN, Book>();

        // Adds a book to the catalog
        public void AddBook(ISBN isbn, Book book)
        {
            books[isbn] = book;
        }

        // Retrieves a book by ISBN
        public Book GetBook(ISBN isbn)
        {
            return books.TryGetValue(isbn, out Book book) ? book : null;
        }

        // a) Get a set of book titles, sorted alphabetically
        public IEnumerable<string> GetSortedTitles()
        {
            return books.Values.Select(book => book.Title).OrderBy(title => title);
        }

        // b) Retrieve a set of books by the specified author name, sorted by publication date
        public IEnumerable<Book> GetBooksByAuthor(string authorName)
        {
            string upperAuthorName = authorName.ToUpper();
            return books.Values
                .Where(book => book.Authors.Contains(upperAuthorName))
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
