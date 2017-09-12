namespace clu.books.library
{
    public interface IBookOutputService
    {
        void LogBookInformation(Book book);

        void LogBooksInformation(Books books);
    }
}
