using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using static System.Console;

/*
Task 6
The task is the extension of task 5.2
To do:
1. Create a simple class to represent a Book. In the book class, store the title of the book (a string, not empty, not null), the publication date of the book (possibly null),
and a collection of Authors (first name, last name, date ofbirth, limitation for names - not more then 200 symbols ) of the book (collection can be empty).
2. The 13-digit ISBN of a book is a string in the format XXX-X-XX-XXXXXX-X or XXXXXXXXXXXXX, where X is thenumber 0..9. 
Create your class Catalog - is a collection, like a dictionary, that stores books. Provide access to the book by key string - by ISBN of the book. Please note an important nuance - if a book was placed in a catalog using the key 123-4-56-789012-3,
then it can be retrieved using both the key 123-4-56-789012-3 and the key 1234567890123.
The correctness of the ISBN itself in this task You don’t have to check (there is no need to checkthe check digits; it is advisable to check the correctness of the format itself-for example, using regularexpressions).

3. Create a Catalog class to store collection of books, catalog can’t contain books with same isbn.


4. Create and fill the Catalog book by real books with authors and isbn (not less then 4 books, some books has to have several authors, author has to have several books)

5. Save catalog object to a xml file (use XMLSerializer class). See sample of xml file.
6. Restore Catalog object from xml file. Check identity to initial object.
7. Save catalog object into json files (one file-one author with all his books.)
8. Restore catalog object from json files. Check identity.
* It is recommended for saving and restoring purposes to create and implement IRepository interface (for example, implemented in XML Repositoty and JSON Repository classes).
Use DAL (data access layer), keep in mind that the task can be extended -> more data sources can be added (for example, SQLRepository), and the task will be - read from xml, save to SQL.
*/

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

namespace Task7
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "books_info.csv");
            filePath = Path.GetFullPath(filePath);

            ILibraryFactory factory = new CsvLibraryFactory(filePath);
            Catalog catalog = factory.GetCatalog();
            var pressReleaseItems = factory.GetPressRelease();
            var library = new Library(catalog, pressReleaseItems);

            // XML Repository
            var xmlRepo = new XMLRepository("Library.xml");
            xmlRepo.Save(catalog);
            var loadedXmlCatalog = xmlRepo.Load();

            // JSON Repository
            var jsonRepo = new JSONRepository("LibraryJson");
            jsonRepo.Save(catalog);
            var loadedJsonCatalog = jsonRepo.Load();

            // Print loaded data
            PrintLibrary(loadedXmlCatalog, "Library from XML");
            PrintLibrary(loadedJsonCatalog, "Library from JSON");
        }

        private static void PrintLibrary(Catalog catalog, string libraryName)
        {
            WriteLine($"Loaded {libraryName}:");
            foreach (var book in catalog.GetAllBooks())
            {
                WriteLine(book);
            }
            WriteLine();
        }
    }
}