namespace Task7
{
    public class DALBook
    {
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<DALAuthor> Authors { get; set; }
        public string ISBN { get; set; }
        public List<string> Publishers { get; set; } // Added to handle PaperBook
        public string InternetResourceId { get; set; } // Specific to EBook
        public List<string> Formats { get; set; } // Specific to EBook

        public static DALBook FromBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            return new DALBook
            {
                Title = book.Title,
                PublicationDate = book.PublicationDate,
                Authors = book.Authors.Select(a => new DALAuthor
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                }).ToList(),
                ISBN = book.ISBN?.ToString() ?? string.Empty,
                Publishers = (book is PaperBook paperBook) ? paperBook.Publishers : null,
                InternetResourceId = (book is EBook ebook) ? ebook.InternetResourceId : null,
                Formats = (book is EBook ebookFormats) ? ebookFormats.Formats : null
            };
        }
        public Book ToBook()
        {
            var authors = Authors.Select(a => new Author(a.FirstName, a.LastName)).ToList();

            if (!string.IsNullOrEmpty(InternetResourceId) && Formats != null)
            {
                return new EBook(Title, PublicationDate, authors, InternetResourceId, Formats);
            }

            ISBN isbn = null;
            try
            {
                isbn = new ISBN(ISBN);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Warning: Invalid ISBN '{ISBN}' for book '{Title}'. Assigning default ISBN. Details: {ex.Message}");
                isbn = new ISBN("000-0-00-000000-0");
            }

            // Ensure Publishers is not null or empty
            var publishers = Publishers ?? new List<string> { "Unknown Publisher" };
            return new PaperBook(Title, PublicationDate, authors, publishers, new List<ISBN> { isbn });
        }
    }
}