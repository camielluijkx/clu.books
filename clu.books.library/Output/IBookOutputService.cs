
using clu.books.library.dto;

namespace clu.books.library.output
{
    public interface IBookOutputService
    {
        void LogBookInformation(Book book);

        void LogBooksInformation(Books books);
    }
}
