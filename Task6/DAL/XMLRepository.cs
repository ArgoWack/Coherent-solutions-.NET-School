using System.Xml.Serialization;
namespace Task6
{
    public class XMLRepository : IRepository
    {
        private readonly string filePath;
        public XMLRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            this.filePath = filePath;
        }
        public void Save(Catalog catalog)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, catalog.GetAllBooks().Values.ToList());
            }
        }
        public Catalog Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                var books = (List<Book>)serializer.Deserialize(reader);
                Catalog catalog = new Catalog();

                foreach (var book in books)
                {
                    catalog.AddBook(book.ISBN, book);
                }
                return catalog;
            }
        }
    }
}