using static System.Console;

/*
Task 5.2.
To do:
1) Create a simple class to represent a book. In the book class, store the title of the book (a string, not empty, not null ), the publication date of the book (possibly null ), and the set of authors of the book (a collection of non - repeating strings, possibly empty).
2) The 13 - digit ISBN of a book is a string in the forma XXX - X - XX - XXXXXX - X or XXXXXXXXXXXXX , where X is the number 0..9. Create your class Catalog - is a collection, like a dictionary, that stores books. Provide access to the book by key string - by ISBN of the book.
Please note an important nuance - if a book was placed in a catalog using the key 123 - 4 - 56 - 789012 - 3, then it can be retrieved using both the key 123 - 4 - 56 - 789012 - 3 and the key 1234567890123.
The correctness of the ISBN itself in this task You don’t have to check (there is no need to check the check digits; it is advisable to check the correctness of the format itself - for example, using regular expressions).
3) Implement the following methods for working with the Directory using LINQ to Objects:
a) Get a set of book titles from the catalog, sorted alphabetically.
b) Retrieve from the catalog a set of books (objects of the corresponding class) by the specified author name. Books should be sorted by publication date.
c) Get from the catalog a set of tuples of the form “author’s name – the number of his books in the catalog” for all authors.
*/

namespace Task52
{
    class Program
    {
        static void Task52()
        {
            // Create a catalog and add some books
            Catalog catalog = new Catalog();

            // Adding books to the catalog
            catalog.AddBook("123-4-56-789012-3", new Book("Book One", new DateTime(2020, 5, 1), new[] { "Author A", "Author B" }));
            catalog.AddBook("9876543210123", new Book("Book Two", new DateTime(2018, 11, 15), new[] { "Author C" }));
            catalog.AddBook("123-4-56-123456-7", new Book("Book Three", null, new[] { "Author A" }));
            catalog.AddBook("123-4-56-789012-5", new Book("Book Four", new DateTime(2020, 7, 1), new[] { "Author A", "Author B" }));

            // Retrieve book by ISBN
            WriteLine("Retrieving books by ISBN:");
            WriteLine("For 123-4-56-789012-3: " + catalog.GetBook("123-4-56-789012-3"));
            WriteLine("For 1234567890123: " + catalog.GetBook("1234567890123"));

            // a) Get sorted book titles
            WriteLine("\nSorted book titles:");
            foreach (string title in catalog.GetSortedTitles())
            {
                WriteLine(title);
            }

            // b) Retrieve books by a specific author
            WriteLine("\nBooks by 'Author A':");
            foreach (Book book in catalog.GetBooksByAuthor("Author A"))
            {
                WriteLine(book);
            }

            // c) Get author book counts
            WriteLine("\nAuthor book counts:");
            foreach (var (author, count) in catalog.GetAuthorBookCounts())
            {
                WriteLine($"{author} - {count} book(s)");
            }
        }
    }
}