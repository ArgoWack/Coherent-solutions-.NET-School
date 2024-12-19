using System.Xml.Serialization;

namespace Task7
{
    public class XMLRepository : IRepository
    {
        private readonly string filePath;
        public XMLRepository(string filePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath);
            this.filePath = filePath;
        }
        public void Save(Catalog catalog)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<DALBook>));
            var dalBooks = catalog.GetAllBooks().Select(DALBook.FromBook).ToList();
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
                    var book = dalBook.ToBook();

                    // Ensure Publishers is populated for PaperBooks
                    if (book is PaperBook paperBook && (paperBook.Publishers == null || !paperBook.Publishers.Any()))
                    {
                        paperBook.Publishers.Add("Unknown Publisher");
                    }
                    catalog.AddBook(book.ISBN.ToString(), book);
                }
                return catalog;
            }
        }
    }
}