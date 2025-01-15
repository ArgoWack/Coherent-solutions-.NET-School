using static System.Console;

/*
Task 7
The task is the extension of task 6
To do:
1)  Add class PaperBook to store the title of the book (a string, not empty, not null), the publication date of the book (possibly null), list of isbns, publisher, 
and a collection of Authors (first name, last name, date of birth(possibly null), limitation for names - not more than 200 symbols) of the book ( collection can be empty).

2) Create a class to represent a EBook (electronic version of book). In the EBook class, store the title of the book (a string, not empty, not null), identifier of internet resource where it is upload, 
list of available electronic formats, and a collection of Authors (first name, last name, date of birth (possibly null), limitation for names - not more than 200 symbols) of the book (collection can be empty).

3) Create class Library, which contains Catalog of books (all books are PaperBook, or all books are EBook ), and PressReleaseItems – list of strings (for PaperBooks catalog - list of all publishers in a Library
, for EBook catalog - list of all available electronic formats in a Library). As a key for a dictionary in catalog use one of the isbns for paper books, for electronic books - identifier (refactor Catalog
class not to check key (isbn) validity, or make outer key validator (prefered))

4) Initialize two Library objects – for PaperBooks with a list of publishers as PressReleaseItems , and EBook with list of formats, using the enclosed text file books_info.csv (can be opened by Excel editor).

5) Save Books to XML and JSON repositories (avoid PaperBook and EBook specific information)

* Do not created classes like ELibrary or PaperLibrary! Use one class Library both paper and e book catalogs.
** It is recommended to use Abstract Factory pattern to create Libraries
 */

/*
Task 8
The task is the extension of task7
To do:
1) Add to class EBook a property Pages to store the number of pages in a book
2) Fill this property using identifier field:
2.1 add https://archive.org/details/ to identifier to achieve the URL for the appropriate electronic book
2.2 download html page using this URL (in async mode), find information in the download file for number of pages: sample <span itemprop="numberOfPages">38</span>
3 Initialize Pages for all books in a Library
*/

namespace Task8
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "books_info.csv");
            filePath = Path.GetFullPath(filePath);

            ILibraryFactory eBookFactory = new EBookLibraryFactory(filePath);
            var eBookCatalog = eBookFactory.GetCatalog();

            var library = new Library(eBookCatalog, eBookFactory.GetPressRelease());

            // Initialize pages for all EBooks in the library
            await EBookPagesScrapper.InitializePagesForLibraryAsync(library);

            // Print results
            WriteLine("EBook Catalog with Pages Initialized:");
            foreach (var book in library.Catalog.GetAllBooks())
            {
                WriteLine(book);
            }
        }
    }
}