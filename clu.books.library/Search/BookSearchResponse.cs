using clu.books.library.dto;

namespace clu.books.library.Search
{
    /// <summary>
    /// Book returned by search.
    /// </summary>
    public class BookSearchResponse
    {
        public Book Book { get; set; }

        public BookSearchResponse(Book book)
        {
            Book = book;
        }
    }
}
