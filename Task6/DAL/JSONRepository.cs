using System.Text.Json;
namespace Task6
{
    public class JSONRepository : IRepository
    {
        private readonly string folderPath;
        public JSONRepository(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path cannot be null or empty.", nameof(folderPath));
            this.folderPath = folderPath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
        public void Save(Catalog catalog)
        {
            foreach (var authorGroup in catalog.GetBooksGroupedByAuthor())
            {
                string filePath = Path.Combine(folderPath, $"{authorGroup.Author.FirstName}_{authorGroup.Author.LastName}.json");

                string json = JsonSerializer.Serialize(authorGroup.Books.Select(book => new
                {
                    book.Title,
                    book.PublicationDate,
                    Authors = book.Authors.Select(a => $"{a.FirstName.ToUpper()} {a.LastName.ToUpper()}"),
                    ISBN = book.ISBN.ToString()
                }), new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, json);
            }
        }
        public Catalog Load()
        {
            Catalog catalog = new Catalog();

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(filePath);
                JsonElement booksJson = JsonSerializer.Deserialize<JsonElement>(json);

                foreach (JsonElement bookElement in booksJson.EnumerateArray())
                {

                    // Parse Title
                    string title = bookElement.GetProperty("Title").GetString();

                    // Parse PublicationDate
                    DateTime? publicationDate = bookElement.TryGetProperty("PublicationDate", out JsonElement pubDateElement) && pubDateElement.ValueKind != JsonValueKind.Null ? pubDateElement.GetDateTime() : null;

                    // Parse ISBN
                    string isbnValue = bookElement.GetProperty("ISBN").GetString();
                    ISBN isbn = new ISBN(isbnValue);

                    // Parse Authors
                    List<Author> authors = new List<Author>();
                    if (bookElement.TryGetProperty("Authors", out JsonElement authorsElement) && authorsElement.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement authorElement in authorsElement.EnumerateArray())
                        {
                            if (authorElement.ValueKind == JsonValueKind.Object)
                            {
                                string firstName = authorElement.GetProperty("FirstName").GetString();
                                string lastName = authorElement.GetProperty("LastName").GetString();
                                DateTime dateOfBirth = authorElement.GetProperty("DateOfBirth").GetDateTime();
                                authors.Add(new Author(firstName, lastName, dateOfBirth));
                            }
                            else if (authorElement.ValueKind == JsonValueKind.String)
                            {
                                string[] parts = authorElement.GetString().Split(", ");
                                if (parts.Length == 2)
                                {
                                    string lastName = parts[0];
                                    string firstName = parts[1];
                                    authors.Add(new Author(firstName, lastName, DateTime.MinValue));
                                }
                            }
                        }
                    }
                    Book book = new Book(title, publicationDate, authors, isbn);
                    catalog.AddBook(isbn, book);
                }
            }
            return catalog;
        }
    }
}