namespace Task6
{
    public class Book
    {
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public HashSet<Author> Authors { get; set; }
        public ISBN ISBN { get; set; }
        public Book()
        {
            Authors = new HashSet<Author>();
        }
        public Book(string title, DateTime? publicationDate, IEnumerable<Author> authors, ISBN isbn)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty or null.");

            Title = title;
            PublicationDate = publicationDate;
            Authors = new HashSet<Author>(authors ?? Enumerable.Empty<Author>());
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
        }
        public override string ToString()
        {
            string authors = Authors.Count > 0
                ? string.Join(", ", Authors.Select(author => $"{author.FirstName} {author.LastName}"))
                : "No authors";
            string date = PublicationDate.HasValue ? PublicationDate.Value.ToShortDateString() : "No publication date";
            return $"{Title} (Published: {date}, Authors: {authors}, ISBN: {ISBN})";
        }
    }
}