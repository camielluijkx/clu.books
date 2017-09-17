using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace clu.books.library.dto
{
    /// <summary>
    /// Representation of a book.
    /// </summary>
    [DataContract]
    public class Book
    {
        /// <summary>
        /// Index on search result.
        /// </summary>
        [DataMember]
        [JsonRequired]
        public int Index { get; set; }

        /// <summary>
        /// Author of book.
        /// </summary>
        [DataMember]
        [JsonRequired]
        public string Author { get; set; }

        /// <summary>
        /// Title of book.
        /// </summary>
        [DataMember]
        [JsonRequired]
        public string Title { get; set; }

        /// <summary>
        /// Date of publication.
        /// </summary>
        [DataMember]
        public string PublishedDate { get; set; }

        /// <summary>
        /// Language code of book.
        /// </summary>
        [DataMember]
        [JsonRequired]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Description of book.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        [JsonIgnore]
        public string Information { get; set; }
    }
}
