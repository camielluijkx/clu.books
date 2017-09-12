using System.Threading.Tasks;
using clu.books.library.model;

namespace clu.books.library.search
{
    public interface IBookSearchService
    {
        Task<Books> SearchBooksByAnythingAsync(string anything);

        Task<Books> SearchBooksByAuthorAsync(string author);

        Task<Book> SearchBookByIsbnAsync(string isbn);

        Task<Books> SearchBooksByIsbnAsync(string isbn);

        Task<Books> SearchBooksByTitleAsync(string title);
    }
}
