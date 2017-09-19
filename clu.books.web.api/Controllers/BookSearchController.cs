using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using clu.books.library.search;
using clu.books.library.settings;
using clu.books.library.Search;
using Swashbuckle.Swagger.Annotations;

namespace clu.books.web.api.controllers
{
    [AllowAnonymous]
    [BooksModelBinding]
    [RoutePrefix("Search")]
    public class BookSearchController : ApiController
    {
        private readonly IConfigurationSettings configurationSettings;

        private readonly IBookSearchService bookSearchService;
        private readonly IBookSearchMapper bookSearchMapper;

        public BookSearchController()
        {
            configurationSettings = new ConfigurationSettings();

            bookSearchMapper = new BookSearchMapper();
            bookSearchMapper.Configure();

            if (configurationSettings.StubSearchResults)
            {
                bookSearchService = new BookSearchServiceStub(bookSearchMapper);
            }
            else
            {
                bookSearchService = new BookSearchService(configurationSettings, bookSearchMapper);
            }
        }

        /// <summary>
        /// Search for a collection of books by author.
        /// </summary>
        /// <param name="author">Name of author.</param>
        /// <remarks>Uses Google Books API to search for books by author.</remarks>
        /// <returns>Collection of books matched by author.</returns>
        /// <response code="200">Search results were found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Author/{author}")]
        [SwaggerResponse(HttpStatusCode.OK, "Books returned by search.", typeof(BooksSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBooksByAuthorAsync(string author)
        {
            try
            {
                BooksSearchRequest searchRequest = new BooksSearchRequest(author, SearchOption.ByAuthor);
                BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for a collection of books by anything.
        /// </summary>
        /// <param name="anything">Any search term.</param>
        /// <remarks>Uses Google Books API to search for books by anything.</remarks>
        /// <returns>Collection of books matched by anything.</returns>
        /// <response code="200">Search results were found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Anything/{anything}")]
        [SwaggerResponse(HttpStatusCode.OK, "Books returned by search.", typeof(BooksSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBooksByAnythingAsync(string anything)
        {
            try
            {
                BooksSearchRequest searchRequest = new BooksSearchRequest(anything, SearchOption.ByAnything);
                BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for a book by isbn.
        /// </summary>
        /// <param name="isbn">ISBN.</param>
        /// <remarks>Uses Google Books API to search for books by ISBN.</remarks>
        /// <returns>Book matched by ISBN.</returns>
        /// <response code="200">Search result was found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Isbn/{isbn}")]
        [SwaggerResponse(HttpStatusCode.OK, "Book returned by search.", typeof(BookSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBookByIsbnAsync(string isbn)
        {
            try
            {
                BookSearchRequest searchRequest = new BookSearchRequest(isbn, SearchOption.ByIsn);
                BookSearchResponse searchResponse = await bookSearchService.SearchBookAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}