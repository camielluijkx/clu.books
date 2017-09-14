using Newtonsoft.Json;

namespace clu.books.library.dto
{
    /// <summary>
    /// Representation of a book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Index on search result.
        /// </summary>
        [JsonRequired]
        public int Index { get; set; }

        /// <summary>
        /// Author of book.
        /// </summary>
        [JsonRequired]
        public string Author { get; set; }

        /// <summary>
        /// Title of book.
        /// </summary>
        [JsonRequired]
        public string Title { get; set; }

        /// <summary>
        /// Date of publication.
        /// </summary>
        [JsonRequired]
        public string PublishedDate { get; set; }

        /// <summary>
        /// Language code of book.
        /// </summary>
        [JsonRequired]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Description of book.
        /// </summary>
        [JsonIgnore]
        public string Description { get; set; }
    }
}
