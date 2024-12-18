namespace Task7
{
    public static class CatalogExtensions
    {
        public static IEnumerable<(Author Author, IEnumerable<Book> Books)> GetBooksGroupedByAuthor(this Catalog catalog)
        {
            return catalog.GetAllBooks()
                .SelectMany(kvp => kvp.Value.Authors.Select(author => (Author: author, Book: kvp.Value)))
                .GroupBy(tuple => tuple.Author)
                .Select(group => (Author: group.Key, Books: group.Select(tuple => tuple.Book)));
        }
    }
}