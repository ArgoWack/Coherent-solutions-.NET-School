namespace Task7
{
    public class PaperBookLibraryFactory : ILibraryFactory
    {
        private readonly CsvReader csvReader;
        public PaperBookLibraryFactory(string filePath) : base(filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            csvReader = new CsvReader(filePath);
        }
        public override Catalog GetCatalog()
        {
            var books = csvReader.GetBooks()
                .Where(book => book is PaperBook)
                .Cast<PaperBook>();
            var catalog = new Catalog();
            foreach (var paperBook in books)
            {
                var isbnKey = paperBook.Isbns.FirstOrDefault()?.ToString() ?? paperBook.Title;
                catalog.AddBook(isbnKey, paperBook);
            }
            return catalog;
        }
        public override List<string> GetPressRelease()
        {
            return csvReader.GetBooks()
                .OfType<PaperBook>()
                .SelectMany(book => book.Publishers)
                .Distinct()
                .ToList();
        }
    }
}