namespace clu.books.library.Search
{
    /// <summary>
    /// Option to use when searching for book(s).
    /// </summary>
    public enum SearchOption
    {
        /// <summary>
        /// Search  by anything.
        /// </summary>
        ByAnything = 0,

        /// <summary>
        /// Search by author.
        /// </summary>
        ByAuthor,

        /// <summary>
        /// Search by ISBN.
        /// </summary>
        ByIsbn,

        /// <summary>
        /// Search by title.
        /// </summary>
        ByTitle
    }
}
