namespace Task8
{
    public class Catalog
    {
        private readonly Dictionary<string, Book> books = new Dictionary<string, Book>();
        public void AddBook(string key, Book book)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            books[key] = book;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            return books.Values;
        }
    }
}