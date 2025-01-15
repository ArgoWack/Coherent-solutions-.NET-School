namespace Task7
{
    public class Library
    {
        public Catalog Catalog { get; private set; }
        public List<string> PressReleaseItems { get; private set; }
        public Library(Catalog catalog, List<string> pressReleaseItems)
        {
            Catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            PressReleaseItems = pressReleaseItems ?? throw new ArgumentNullException(nameof(pressReleaseItems));
        }
    }
}