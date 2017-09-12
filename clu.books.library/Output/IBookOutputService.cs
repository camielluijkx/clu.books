using clu.books.library.model;

namespace clu.books.library.output
{
    public interface IBookOutputService
    {
        void LogBookInformation(Book book);

        void LogBooksInformation(Books books);
    }
}
