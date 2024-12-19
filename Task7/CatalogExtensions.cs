namespace Task7
{
    public static class CatalogExtensions
    {
        public static IEnumerable<(Author Author, IEnumerable<Book> Books)> GetBooksGroupedByAuthor(this Catalog catalog)
        {
            return catalog.GetAllBooks()
                .SelectMany(book => book.Authors.Select(author => (Author: author, Book: book)))
                .GroupBy(tuple => tuple.Author)
                .Select(group => (Author: group.Key, Books: group.Select(tuple => tuple.Book)));
        }
    }
}