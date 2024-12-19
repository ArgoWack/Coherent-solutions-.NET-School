namespace Task7
{
    public class EBookLibraryFactory : ILibraryFactory
    {
        public Library CreateLibrary()
        {
            var catalog = new Catalog();

            catalog.AddBook("hobbit-res-001",new EBook("The Hobbit: or There and Back Again", new DateTime(2020, 5, 1),new[] { new Author("J.R.R.", "Tolkien", new DateTime(1892, 1, 3)) },"hobbit-res-001",new List<string> { "PDF", "EPUB", "MOBI" }));
            catalog.AddBook("two-towers-res-002",new EBook("The Two Towers The Lord of the Rings", new DateTime(1986, 11, 15), new[] { new Author("J.R.R.", "Tolkien", new DateTime(1892, 1, 3)) },"two-towers-res-002",new List<string> { "EPUB", "AZW3" }));
            catalog.AddBook("linq-pocket-ref-res-003",new EBook("Linq Pocket Reference: Learn and Implement Linq for .Net Applications", null,new[] { new Author("Joseph", "Albahari", new DateTime(1970, 1, 1)), new Author("Ben", "Albahari", new DateTime(1975, 1, 1)) },"linq-pocket-ref-res-003",new List<string> { "PDF", "MOBI" }));
            catalog.AddBook("csharp-7-res-004",new EBook("C# 7.0 Pocket Reference: Instant Help for C# 7.0 Programmers", new DateTime(2017, 7, 1),new[] { new Author("Joseph", "Albahari", new DateTime(1970, 1, 1)) },"csharp-7-res-004",new List<string> { "PDF", "EPUB" }));
            catalog.AddBook("csharp-8-res-005",new EBook("C# 8.0 in a Nutshell: The Definitive Reference", new DateTime(2020, 1, 1),new[] { new Author("Joseph", "Albahari", new DateTime(1970, 1, 1)) },"csharp-8-res-005",new List<string> { "AZW3", "PDF" }));
            catalog.AddBook("war-jewels-res-006",new EBook("The War of the Jewels: The Later Silmarilion - Part Two - Volume XI: The History of Middle-Earth", new DateTime(1994, 3, 1),new[] { new Author("J.R.R.", "Tolkien", new DateTime(1892, 1, 3)), new Author("Christopher", "Tolkien", new DateTime(1924, 11, 21)) },"war-jewels-res-006",new List<string> { "EPUB", "PDF" }));

            return new Library(catalog, new List<string> { "PDF", "EPUB", "MOBI", "AZW3" });
        }
    }
}
