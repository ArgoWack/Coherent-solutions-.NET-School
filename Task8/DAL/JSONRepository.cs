using System.Text.Json;
using static System.Console;

namespace Task8
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
                // Validate books before saving
                if (!Validation.IsValidBook(book))
                {
                    WriteLine($"Skipping invalid book '{book.Title}' while saving.");
                    continue;
                }
                var dalBook = DALBook.FromBook(book);
                string sanitizedFileName = GetSafeFileName(dalBook.Title) + ".json";
                string filePath = Path.Combine(folderPath, sanitizedFileName);
                // Serialize to JSON
                string json = JsonSerializer.Serialize(dalBook, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
        }
        public Catalog Load()
        {
            var catalog = new Catalog();

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    var dalBook = JsonSerializer.Deserialize<DALBook>(json);

                    if (dalBook == null)
                    {
                        WriteLine($"Skipping invalid JSON file: {filePath}");
                        continue;
                    }

                    var book = dalBook.ToBook();
                    catalog.AddBook(book.ISBN?.ToString() ?? dalBook.Title, book);
                }
                catch (Exception ex)
                {
                    WriteLine($"Error loading JSON file '{filePath}': {ex.Message}");
                }
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