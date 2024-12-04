namespace Task6
{
    public class Catalog
    {
        private readonly Dictionary<ISBN, Book> books = new Dictionary<ISBN, Book>();
        public void AddBook(ISBN isbn, Book book)
        {
            books[isbn] = book;
        }
        public Book GetBook(ISBN isbn)
        {
            return books.TryGetValue(isbn, out Book book) ? book : null;
        }
        public IEnumerable<string> GetSortedTitles()
        {
            return books.Values.Select(book => book.Title).OrderBy(title => title);
        }
        public IEnumerable<Book> GetBooksByAuthor(string authorName)
        {
            return books.Values
                .Where(book => book.Authors.Any(author =>
                    $"{author.FirstName} {author.LastName}".ToUpper() == authorName.ToUpper()))
                .OrderBy(book => book.PublicationDate);
        }
        public IEnumerable<(Author Author, int BookCount)> GetAuthorBookCounts()
        {
            return books.Values
                .SelectMany(book => book.Authors)
                .GroupBy(author => author)
                .Select(group => (Author: group.Key, BookCount: group.Count()));
        }
        public Dictionary<ISBN, Book> GetAllBooks()
        {
            return new Dictionary<ISBN, Book>(books);
        }
    }
}