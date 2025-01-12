using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using static System.Console;

namespace Task7
{
    public class CsvLibraryFactory : ILibraryFactory
    {
        public CsvLibraryFactory(string filePath) : base(filePath) { }
        public override Catalog GetCatalog()
        {
            var catalog = new Catalog();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    // Extract fields
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
                    // Handle PaperBooks
                    if (!string.IsNullOrEmpty(relatedId))
                    {
                        var isbns = ExtractISBNs(relatedId);
                        if (!isbns.Any())
                        {
                            WriteLine($"Skipping PaperBook entry: Invalid ISBNs for title '{title}'.");
                            continue;
                        }
                        catalog.AddBook(
                            isbns.First().ToString(),
                            new PaperBook(
                                title,
                                publicationDate,
                                authors,
                                string.IsNullOrWhiteSpace(publisher) ? new List<string>() : new List<string> { publisher },
                                isbns
                            )
                        );
                    }
                    // Handle EBooks
                    else if (!string.IsNullOrEmpty(identifier))
                    {
                        if (string.IsNullOrWhiteSpace(format))
                        {
                            WriteLine($"Skipping EBook entry: Missing formats for title '{title}'.");
                            continue;
                        }
                        var formats = format.Split(',').Select(f => f.Trim()).ToList();

                        catalog.AddBook(
                            identifier,
                            new EBook(
                                title,
                                publicationDate,
                                authors,
                                identifier,
                                formats
                            )
                        );
                    }
                    else
                    {
                        WriteLine($"Skipping entry: Missing both related-external-id and identifier for title '{title}'.");
                    }
                }
            }
            return catalog;
        }
        public override List<string> GetPressRelease()
        {
            var catalog = GetCatalog();
            return catalog.GetAllBooks().OfType<PaperBook>()
                .SelectMany(b => b.Publishers)
                .Union(catalog.GetAllBooks().OfType<EBook>().SelectMany(b => b.Formats))
                .Distinct()
                .ToList();
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