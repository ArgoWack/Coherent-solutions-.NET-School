namespace Task6
{
    public class Book
    {
        public string Title { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public HashSet<Author> Authors { get; private set; }
        public ISBN ISBN { get; private set; }

        public Book(string title, DateTime? publicationDate, IEnumerable<Author> authors, ISBN isbn)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty or null.");

            Title = title;
            PublicationDate = publicationDate;
            Authors = new HashSet<Author>(authors ?? Enumerable.Empty<Author>());
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
        }
        // Conversion to DALBook
        public DALBook ToDAL()
        {
            return new DALBook
            {
                Title = Title,
                PublicationDate = PublicationDate,
                Authors = Authors.Select(a => new DALAuthor
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    DateOfBirth = a.DateOfBirth
                }).ToList(),
                ISBN = ISBN.ToString()
            };
        }
        // Conversion from DALBook
        public static Book FromDAL(DALBook dalBook)
        {
            var authors = dalBook.Authors.Select(a => new Author(a.FirstName, a.LastName, a.DateOfBirth)).ToList();
            return new Book(dalBook.Title, dalBook.PublicationDate, authors, new ISBN(dalBook.ISBN));
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