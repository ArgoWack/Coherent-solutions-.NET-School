using System.Text.RegularExpressions;
using static System.Console;

namespace Task8
{
    public class EBookPagesScrapper
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public static async Task<int?> FetchPagesAsync(EBook eBook)
        {
            if (string.IsNullOrWhiteSpace(eBook.InternetResourceId))
            {
                WriteLine($"EBook '{eBook.Title}' has no InternetResourceId.");
                return null;
            }
            string url = $"https://archive.org/details/{eBook.InternetResourceId}";
            WriteLine($"Fetching URL: {url}");
            string htmlContent = await httpClient.GetStringAsync(url);
            var match = Regex.Match(htmlContent, @"<span[^>]*itemprop=""numberOfPages""[^>]*>(\d+)</span>");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int pages))
            {
                return pages;
            }
            else
            {
                WriteLine($"Failed to extract the number of pages for EBook: {eBook.Title}");
            }
            return null;
        }
        public static async Task InitializePagesForLibraryAsync(Library library)
        {
            if (library == null)
                throw new ArgumentNullException(nameof(library));
            var fetchTasks = new List<Task>();
            foreach (var book in library.Catalog.GetAllBooks())
            {
                if (book is EBook eBook)
                {
                    fetchTasks.Add(FetchAndSetPagesAsync(eBook));
                }
            }
            await Task.WhenAll(fetchTasks); // awaits for all fetches to complete
        }
        private static async Task FetchAndSetPagesAsync(EBook eBook)
        {
            //speeds up loading in async way
            eBook.Pages = await FetchPagesAsync(eBook);
        }
    }
}