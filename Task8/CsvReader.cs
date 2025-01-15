using System.Globalization;
using CsvHelper.Configuration;
using static System.Console;
namespace Task8
{
    public class CsvReader
    {
        private readonly string filePath;
        public CsvReader(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            this.filePath = filePath;
        }
        public IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(reader, config))
            {
                if (!csv.Read())
                {
                    throw new InvalidOperationException("CSV file is empty or invalid.");
                }
                csv.ReadHeader();
                while (csv.Read())
                {
                    string title = csv.GetField("title");
                    string publicDate = csv.GetField("publicdate");
                    string authorsField = csv.GetField("creator");
                    string relatedId = csv.GetField("related-external-id");
                    string identifier = csv.GetField("identifier");
                    string format = csv.GetField("format");
                    string publisher = csv.GetField("publisher");
                    // Validate required fields
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        WriteLine($"Skipping entry: Missing title.");
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(authorsField))
                    {
                        WriteLine($"Skipping entry: Missing authors for title '{title}'.");
                        continue;
                    }
                    var authors = ParseAuthors(authorsField);
                    if (!authors.Any())
                    {
                        WriteLine($"Skipping entry: Invalid authors for title '{title}'.");
                        continue;
                    }
                    // Parse publication date
                    DateTime? publicationDate = null;
                    if (!string.IsNullOrEmpty(publicDate))
                    {
                        if (!DateTime.TryParse(publicDate, out var parsedDate))
                        {
                            WriteLine($"Skipping entry: Invalid publication date '{publicDate}' for title '{title}'.");
                            continue;
                        }
                        publicationDate = parsedDate;
                    }
                    if (!string.IsNullOrEmpty(relatedId))
                    {
                        var isbns = ExtractISBNs(relatedId);
                        if (!isbns.Any())
                        {
                            WriteLine($"Skipping PaperBook entry: Invalid ISBNs for title '{title}'.");
                            continue;
                        }
                        books.Add(new PaperBook(
                            title,
                            publicationDate,
                            authors,
                            string.IsNullOrWhiteSpace(publisher) ? new List<string>() : new List<string> { publisher },
                            isbns
                        ));
                    }
                    else if (!string.IsNullOrEmpty(identifier) && !string.IsNullOrWhiteSpace(format))
                    {
                        var formats = format.Split(',').Select(f => f.Trim()).ToList();
                        books.Add(new EBook(
                            title,
                            publicationDate,
                            authors,
                            identifier,
                            formats
                        ));
                    }
                    else
                    {
                        WriteLine($"Skipping entry: Missing both related-external-id and identifier for title '{title}'.");
                    }
                }
            }
            return books;
        }
        private List<Author> ParseAuthors(string authorsField)
        {
            var authors = new List<Author>();
            foreach (var authorData in authorsField.Split(','))
            {
                var nameParts = authorData.Trim().Split(' ');
                if (nameParts.Length == 1)
                {
                    authors.Add(new Author(nameParts[0], "Unknown"));
                }
                else if (nameParts.Length > 1)
                {
                    string firstName = nameParts[0];
                    string lastName = string.Join(" ", nameParts.Skip(1));
                    authors.Add(new Author(firstName, lastName));
                }
            }
            return authors;
        }
        private DateTime? ParsePublicationDate(string date)
        {
            if (DateTime.TryParse(date, out var parsedDate))
                return parsedDate;
            return null;
        }
        private List<ISBN> ExtractISBNs(string relatedId)
        {
            var isbns = new List<ISBN>();
            foreach (var id in relatedId.Split(','))
            {
                if (id.StartsWith("urn:isbn:"))
                {
                    var isbnValue = id.Replace("urn:isbn:", "");
                    try
                    {
                        isbns.Add(new ISBN(isbnValue));
                    }
                    catch (ArgumentException ex)
                    {
                        WriteLine($"Warning: {ex.Message}. Skipping invalid ISBN '{isbnValue}'.");
                    }
                }
            }
            return isbns;
        }
    }
}