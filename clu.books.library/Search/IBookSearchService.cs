using System.Threading.Tasks;
using clu.books.library.Search;

namespace clu.books.library.search
{
    public interface IBookSearchService
    {
        Task<BookSearchResponse> SearchBookAsync(BookSearchRequest searchRequest);

        Task<BooksSearchResponse> SearchBooksAsync(BooksSearchRequest searchRequest);
    }
}
