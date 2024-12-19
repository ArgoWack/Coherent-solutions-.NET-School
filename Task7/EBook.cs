namespace Task7
{
    public class EBook : Book
    {
        public string InternetResourceId { get; private set; }
        public List<string> Formats { get; private set; }
        public EBook(string title, DateTime? publicationDate, IEnumerable<Author> authors, string internetResourceId, List<string> formats)
            : base(title, publicationDate, authors, null)
        {
            ArgumentException.ThrowIfNullOrEmpty(internetResourceId);

            if (formats == null || formats.Count == 0)
                throw new ArgumentException("Formats cannot be null or empty.");

            InternetResourceId = internetResourceId;
            Formats = formats;
        }
        public override string ToString()
        {
            string formats = string.Join(", ", Formats);
            return $"{base.ToString()}, ResourceId: {InternetResourceId}, Formats: {formats}";
        }
    }
}