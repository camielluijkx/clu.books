namespace clu.books.library.Search
{
    /// <summary>
    /// Search for specific book.
    /// </summary>
    public class BookSearchRequest
    {
        public string SearchTerm { get; set; }

        public SearchOption SearchOption { get; set; }

        public BookSearchRequest(string searchTerm, SearchOption searchOption)
        {
            SearchTerm = searchTerm;
            SearchOption = searchOption;
        }
    }
}
