using clu.books.library.dto;

namespace clu.books.library.Search
{
    /// <summary>
    /// Collection of books returned by search.
    /// </summary>
    public class BooksSearchResponse
    {
        public Books Books { get; set; }

        public BooksSearchResponse(Books books)
        {
            Books = books;
        }
    }
}
