using System.Text.Json;

namespace Task6
{
    public class JSONRepository : IRepository
    {
        private readonly string folderPath;
        public JSONRepository(string folderPath)
        {
            ArgumentException.ThrowIfNullOrEmpty(folderPath);

            this.folderPath = folderPath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
        public void Save(Catalog catalog)
        {
            foreach (var authorGroup in catalog.GetBooksGroupedByAuthor())
            {
                string filePath = Path.Combine(folderPath, $"{authorGroup.Author.FirstName}_{authorGroup.Author.LastName}.json");
                var dalBooks = authorGroup.Books.Select(DALBook.FromBook).ToList();
                string json = JsonSerializer.Serialize(dalBooks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
        }

        public Catalog Load()
        {
            Catalog catalog = new Catalog();
            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(filePath);
                var dalBooks = JsonSerializer.Deserialize<List<DALBook>>(json);
                foreach (var dalBook in dalBooks)
                {
                    var book = dalBook.ToBook();
                    catalog.AddBook(book.ISBN, book);
                }
            }
            return catalog;
        }
    }
}