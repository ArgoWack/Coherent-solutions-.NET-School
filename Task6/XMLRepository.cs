using System.Xml.Serialization;

namespace Task6
{
    public class XMLRepository : IRepository
    {
        public void Save(Catalog catalog, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogData));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, CatalogData.FromCatalog(catalog));
            }
        }

        public Catalog Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CatalogData));
            using (StreamReader reader = new StreamReader(filePath))
            {
                CatalogData catalogData = (CatalogData)serializer.Deserialize(reader);
                return catalogData.ToCatalog();
            }
        }
    }

    [Serializable]
    public class CatalogData
    {
        public List<BookData> Books { get; set; } = new List<BookData>();

        public static CatalogData FromCatalog(Catalog catalog)
        {
            return new CatalogData
            {
                Books = catalog.GetAllBooks().Select(book => BookData.FromBook(book.Key, book.Value)).ToList()
            };
        }

        public Catalog ToCatalog()
        {
            Catalog catalog = new Catalog();
            foreach (var bookData in Books)
            {
                catalog.AddBook(new ISBN(bookData.ISBN), bookData.ToBook());
            }
            return catalog;
        }
    }

    [Serializable]
    public class BookData
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<string> Authors { get; set; } = new List<string>();

        public static BookData FromBook(ISBN isbn, Book book)
        {
            return new BookData
            {
                ISBN = isbn.ToString(),
                Title = book.Title,
                PublicationDate = book.PublicationDate,
                Authors = book.Authors.ToList()
            };
        }

        public Book ToBook()
        {
            return new Book(Title, PublicationDate, Authors);
        }
    }
}