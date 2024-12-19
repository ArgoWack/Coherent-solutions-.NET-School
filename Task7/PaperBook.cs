namespace Task7
{
    public class PaperBook : Book
    {
        public List<string> Publishers { get; private set; }
        public HashSet<ISBN> Isbns { get; private set; }
        public PaperBook(string title, DateTime? publicationDate, IEnumerable<Author> authors, List<string> publishers, List<ISBN> isbns): base(title, publicationDate, authors, isbns.First())
        {
            if (publishers == null || publishers.Count == 0)
                throw new ArgumentException("Publishers cannot be null or empty.");

            Publishers = publishers;
            Isbns = new HashSet<ISBN>(isbns);
        }
        public override string ToString()
        {
            string publishers = string.Join(", ", Publishers);
            return $"{base.ToString()}, Publishers: {publishers}";
        }
    }
}