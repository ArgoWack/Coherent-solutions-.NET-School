namespace Task7
{
    public class PaperLibraryFactory : ILibraryFactory
    {
        public Library CreateLibrary()
        {
            var catalog = new Catalog();

            catalog.AddBook("978-0-61-800221-4",new PaperBook("The Hobbit: or There and Back Again", new DateTime(2020, 5, 1),new[] { new Author("J.R.R.", "Tolkien", new DateTime(1892, 1, 3)) },new List<string> { "Houghton Mifflin Harcourt" },new List<ISBN> { new ISBN("978-0-61-800221-4") }));
            catalog.AddBook("9780345339713",new PaperBook("The Two Towers The Lord of the Rings", new DateTime(1986, 11, 15),new[] { new Author("J.R.R.", "Tolkien", new DateTime(1892, 1, 3)) },new List<string> { "Del Rey Books" },new List<ISBN> { new ISBN("9780345339713") }));
            catalog.AddBook("9780596519247",new PaperBook("Linq Pocket Reference: Learn and Implement Linq for .Net Applications", null,new[] { new Author("Joseph", "Albahari", new DateTime(1970, 1, 1)), new Author("Ben", "Albahari", new DateTime(1975, 1, 1)) },new List<string> { "O'Reilly Media" },new List<ISBN> { new ISBN("9780596519247") }));
            catalog.AddBook("9781491988534",new PaperBook("C# 7.0 Pocket Reference: Instant Help for C# 7.0 Programmers", new DateTime(2017, 7, 1),new[] { new Author("Joseph", "Albahari", new DateTime(1970, 1, 1)) },new List<string> { "O'Reilly Media" },new List<ISBN> { new ISBN("9781491988534") }));
            catalog.AddBook("9781492051138",new PaperBook("C# 8.0 in a Nutshell: The Definitive Reference", new DateTime(2020, 1, 1),new[] { new Author("Joseph", "Albahari", new DateTime(1970, 1, 1)) },new List<string> { "O'Reilly Media" },new List<ISBN> { new ISBN("9781492051138") }));
            catalog.AddBook("9780395710418",new PaperBook("The War of the Jewels: The Later Silmarilion - Part Two - Volume XI: The History of Middle-Earth", new DateTime(1994, 3, 1),new[] { new Author("J.R.R.", "Tolkien", new DateTime(1892, 1, 3)), new Author("Christopher", "Tolkien", new DateTime(1924, 11, 21)) },new List<string> { "Mariner Books" },new List<ISBN> { new ISBN("9780395710418") }));

            return new Library(catalog, new List<string> { "Houghton Mifflin Harcourt", "Del Rey Books", "O'Reilly Media", "Mariner Books" });
        }
    }
}