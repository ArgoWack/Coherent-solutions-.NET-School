namespace Task8
{
    class Validation
    {
        public static bool IsValidBook(Book book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.Title) || book.Authors == null || !book.Authors.Any())
                return false;
            if (book is PaperBook paperBook)
            {
                return paperBook.Publishers != null && paperBook.Publishers.Any() &&
                       paperBook.Isbns != null && paperBook.Isbns.Any();
            }
            if (book is EBook eBook)
            {
                return !string.IsNullOrWhiteSpace(eBook.InternetResourceId) &&
                       eBook.Formats != null && eBook.Formats.Any();
            }
            return false;
        }
    }
}