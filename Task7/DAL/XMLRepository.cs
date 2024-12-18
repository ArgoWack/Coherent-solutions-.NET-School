using System.Xml.Serialization;

namespace Task7
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<DALBook>));
            var dalBooks = catalog.GetAllBooks().Values.Select(book => book.ToDAL()).ToList();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, dalBooks);
            }
        }
        public Catalog Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<DALBook>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                var dalBooks = (List<DALBook>)serializer.Deserialize(reader);
                Catalog catalog = new Catalog();
                foreach (var dalBook in dalBooks)
                {
                    var book = Book.FromDAL(dalBook);
                    catalog.AddBook(book.ISBN, book);
                }
                return catalog;
            }
        }
    }
}