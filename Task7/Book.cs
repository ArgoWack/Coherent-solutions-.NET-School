namespace Task7
{
    public class Book
    {
        public string Title { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public HashSet<Author> Authors { get; private set; }
        public ISBN ISBN { get; private set; }

        public Book(string title, DateTime? publicationDate, IEnumerable<Author> authors, ISBN isbn)
        {
            ArgumentException.ThrowIfNullOrEmpty(title);

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