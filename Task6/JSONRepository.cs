using System.Text.Json;

namespace Task6
{
    public class JSONRepository : IRepository
    {
        public void Save(Catalog catalog, string folderPath)
        {
            foreach (var authorGroup in catalog.GetBooksGroupedByAuthor())
            {
                string filePath = Path.Combine(folderPath, $"{authorGroup.Author.Replace(" ", "_")}.json");
                string json = JsonSerializer.Serialize(authorGroup.Books.Select(book => new
                {
                    Title = book.Title,
                    PublicationDate = book.PublicationDate,
                    Authors = book.Authors,
                    ISBN = catalog.GetAllBooks().FirstOrDefault(kvp => kvp.Value == book).Key.ToString()
                }), new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
        }

        public Catalog Load(string folderPath)
        {
            Catalog catalog = new Catalog();
            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(filePath);
                var books = JsonSerializer.Deserialize<List<BookData>>(json);
                foreach (var bookData in books)
                {
                    catalog.AddBook(new ISBN(bookData.ISBN), bookData.ToBook());
                }
            }
            return catalog;
        }
    }
}