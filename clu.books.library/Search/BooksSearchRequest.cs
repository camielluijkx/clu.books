namespace clu.books.library.Search
{
    /// <summary>
    /// Search for specific books.
    /// </summary>
    public class BooksSearchRequest
    {
        public string SearchTerm { get; set; }

        public SearchOption SearchOption { get; set; }

        public BooksSearchRequest(string searchTerm, SearchOption searchOption)
        {
            SearchTerm = searchTerm;
            SearchOption = searchOption;
        }
    }
}
