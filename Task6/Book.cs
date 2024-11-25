
namespace Task6
{
    public class Book
    {
        public string Title { get; }
        public DateTime? PublicationDate { get; }
        public HashSet<string> Authors { get; }

        public Book(string title, DateTime? publicationDate, IEnumerable<string> authors)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty or null.");

            Title = title;
            PublicationDate = publicationDate;
            Authors = new HashSet<string>(authors?.Select(a => a.ToUpper()) ?? Enumerable.Empty<string>());
        }

        public override string ToString()
        {
            string authors = Authors.Count > 0 ? string.Join(", ", Authors) : "No authors";
            string date = PublicationDate.HasValue ? PublicationDate.Value.ToShortDateString() : "No publication date";
            return $"{Title} (Published: {date}, Authors: {authors})";
        }
    }
}