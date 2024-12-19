namespace Task6
{
    public class DALBook
    {
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<DALAuthor> Authors { get; set; }
        public string ISBN { get; set; }

        // Conversion from Book to DALBook
        public static DALBook FromBook(Book book)
        {
            return new DALBook
            {
                Title = book.Title,
                PublicationDate = book.PublicationDate,
                Authors = book.Authors.Select(a => new DALAuthor
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    DateOfBirth = a.DateOfBirth
                }).ToList(),
                ISBN = book.ISBN.ToString()
            };
        }

        // Conversion from DALBook to Book
        public Book ToBook()
        {
            var authors = Authors.Select(a => new Author(a.FirstName, a.LastName, a.DateOfBirth)).ToList();
            return new Book(Title, PublicationDate, authors, new ISBN(ISBN));
        }
    }
}