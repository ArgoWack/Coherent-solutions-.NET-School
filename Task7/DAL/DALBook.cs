namespace Task7
{
    public class DALBook
    {
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public List<DALAuthor> Authors { get; set; }
        public string ISBN { get; set; }
    }
}