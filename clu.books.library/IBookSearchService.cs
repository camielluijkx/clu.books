using System.Threading.Tasks;

namespace clu.books.library
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
