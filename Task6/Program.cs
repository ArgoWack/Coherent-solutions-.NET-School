using static System.Console;

/*
Task 5.2.
Done:
1) Create a simple class to represent a book. In the book class, store the title of the book (a string, not empty, not null ), the publication date of the book (possibly null ), and the set of authors of the book (a collection of non - repeating strings, possibly empty).
2) The 13 - digit ISBN of a book is a string in the forma XXX - X - XX - XXXXXX - X or XXXXXXXXXXXXX , where X is the number 0..9. Create your class Catalog - is a collection, like a dictionary, that stores books. Provide access to the book by key string - by ISBN of the book.
Please note an important nuance - if a book was placed in a catalog using the key 123 - 4 - 56 - 789012 - 3, then it can be retrieved using both the key 123 - 4 - 56 - 789012 - 3 and the key 1234567890123.
The correctness of the ISBN itself in this task You don’t have to check (there is no need to check the check digits; it is advisable to check the correctness of the format itself - for example, using regular expressions).
3) Implement the following methods for working with the Directory using LINQ to Objects:
a) Get a set of book titles from the catalog, sorted alphabetically.
b) Retrieve from the catalog a set of books (objects of the corresponding class) by the specified author name. Books should be sorted by publication date.
c) Get from the catalog a set of tuples of the form “author’s name – the number of his books in the catalog” for all authors.
*/

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
namespace Task6
{
    class Program
    {
        // In case of multiple books with the same ISBN the last one is being kept
        static void Task6()
        {
            // Create catalog and populate it with books
            Catalog catalog = new Catalog();
            catalog.AddBook(new ISBN("978-0-61-800221-4"), new Book("The Hobbit: or There and Back Again", new DateTime(1999, 5, 1), new[] { "J.R.R.Tolkien"}));
            catalog.AddBook(new ISBN("9780345339713"), new Book("The Two Towers The Lord of the Rings", new DateTime(1986, 11, 15), new[] { "J.R.R.Tolkien" }));
            catalog.AddBook(new ISBN("9780596519247"), new Book("Linq Pocket Reference: Learn and Implement Linq for .Net Applications", null, new[] { "Albahari, Joseph", "Albahari, Ben" }));
            catalog.AddBook(new ISBN("9781491988534"), new Book("C# 7.0 Pocket Reference: Instant Help for C# 7.0 Programmers", new DateTime(2017, 7, 1), new[] { "Albahari, Joseph" }));
            catalog.AddBook(new ISBN("9781492051138"), new Book("C# 8.0 in a Nutshell: The Definitive Reference", new DateTime(2020, 1, 1), new[] { "Albahari, Joseph" }));
            catalog.AddBook(new ISBN("978-0-61-800221-4"), new Book("The Hobbit: or There and Back Again", new DateTime(2020, 5, 1), new[] { "J.R.R.Tolkien" }));
            catalog.AddBook(new ISBN("978-0-39-571041-8"), new Book("The War of the Jewels: The Later Silmarilion - Part Two - Volume XI: The History of Middle-Earth", new DateTime(1994, 3, 1), new[] { "J.R.R.Tolkien", "Christopher Tolkien (Editor)" }));

            // Save and Load using XML
            IRepository xmlRepo = new XMLRepository();
            string xmlPath = "catalog.xml";

            xmlRepo.Save(catalog, xmlPath);

            Catalog loadedXmlCatalog = xmlRepo.Load(xmlPath);
            WriteLine("\nLoaded from XML:");
            PrintCatalog(loadedXmlCatalog);

            // Save and Load using JSON
            IRepository jsonRepo = new JSONRepository();
            string jsonFolderPath = "JsonCatalog";
            Directory.CreateDirectory(jsonFolderPath);
            jsonRepo.Save(catalog, jsonFolderPath);
            Catalog loadedJsonCatalog = jsonRepo.Load(jsonFolderPath);
            WriteLine("\nLoaded from JSON:");
            PrintCatalog(loadedJsonCatalog);
        }

        private static void PrintCatalog(Catalog catalog)
        {
            foreach (var book in catalog.GetAllBooks())
            {
                WriteLine($"{book.Key}: {book.Value}");
            }
        }
    }
}