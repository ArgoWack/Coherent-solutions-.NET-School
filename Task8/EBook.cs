namespace Task8
{
    public class EBook : Book
    {
        public string InternetResourceId { get; private set; }
        public List<string> Formats { get; private set; }
        public int? Pages { get; set; } //nullable int for pages
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
            return $"{base.ToString()}, Pages: {Pages?.ToString() ?? "Unknown"}, ResourceId: {InternetResourceId}, Formats: {formats}";
        }
    }
}