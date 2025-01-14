namespace Task7
{
    public class EBookLibraryFactory : ILibraryFactory
    {
        private readonly CsvReader csvReader;
        public EBookLibraryFactory(string filePath) : base(filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            csvReader = new CsvReader(filePath);
        }
        public override Catalog GetCatalog()
        {
            var books = csvReader.GetBooks()
                .Where(book => book is EBook)
                .Cast<EBook>();
            var catalog = new Catalog();
            foreach (var eBook in books)
            {
                var identifierKey = eBook.InternetResourceId ?? eBook.Title;
                catalog.AddBook(identifierKey, eBook);
            }
            return catalog;
        }
        public override List<string> GetPressRelease()
        {
            return csvReader.GetBooks()
                .OfType<EBook>()
                .SelectMany(book => book.Formats)
                .Distinct()
                .ToList();
        }
    }
}