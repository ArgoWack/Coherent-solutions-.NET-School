using System.Text.Json;

namespace Task7
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
            foreach (var book in catalog.GetAllBooks())
            {
                var dalBook = DALBook.FromBook(book);
                string sanitizedFileName = GetSafeFileName(dalBook.Title) + ".json";
                string filePath = Path.Combine(folderPath, sanitizedFileName);
                string json = JsonSerializer.Serialize(dalBook, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
        }
        public Catalog Load()
        {
            var catalog = new Catalog();
            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(filePath);
                var dalBook = JsonSerializer.Deserialize<DALBook>(json);
                var book = dalBook.ToBook();
                catalog.AddBook(book.ISBN?.ToString() ?? dalBook.Title, book); // Use Title as fallback key
            }
            return catalog;
        }
        private string GetSafeFileName(string name)
        {
            // Replace invalid characters with an underscore
            return string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
        }
    }
}