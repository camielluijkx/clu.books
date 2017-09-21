using clu.books.library.search;

namespace clu.books.library.Search
{
    public interface IBookSearchServiceFactory
    {
        IBookSearchService Create();
    }
}
