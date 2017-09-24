using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using clu.books.library.dto;
using clu.books.library.Logging;
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

        private readonly IBookSearchServiceFactory bookSearchServiceFactory;
        private readonly IBookSearchService bookSearchService;
        private readonly IBookSearchMapper bookSearchMapper;

        private readonly ILogger logger;

        public BookSearchController() // [TODO] improve ioc setup
        {
            configurationSettings = new ConfigurationSettings();

            bookSearchMapper = new BookSearchMapper();
            bookSearchMapper.Configure();

            bookSearchServiceFactory = new BookSearchServiceFactory(configurationSettings, bookSearchMapper);
            bookSearchService = bookSearchServiceFactory.Create();

            logger = new Logger(configurationSettings);
        }

        /// <summary>
        /// Search for book by author.
        /// </summary>
        /// <param name="author">Any search term.</param>
        /// <remarks>Uses Google Books API to search for book by author.</remarks>
        /// <returns>Book matched by author.</returns>
        /// <response code="200">Search result was found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Book/Author/{author}")]
        [SwaggerResponse(HttpStatusCode.OK, "Book returned by search.", typeof(BookSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBookByAuthorAsync(string author)
        {
            try
            {
                await logger.LogInfoAsync($"Search book by author: {author}.");
                BookSearchRequest searchRequest = new BookSearchRequest(author, SearchOption.ByAuthor);
                BookSearchResponse searchResponse = await bookSearchService.SearchBookAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for books by author.
        /// </summary>
        /// <param name="author">Name of author.</param>
        /// <remarks>Uses Google Books API to search for books by author.</remarks>
        /// <returns>Books matched by author.</returns>
        /// <response code="200">Search results were found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Books/Author/{author}")]
        [SwaggerResponse(HttpStatusCode.OK, "Books returned by search.", typeof(BooksSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBooksByAuthorAsync(string author)
        {
            try
            {
                await logger.LogInfoAsync($"Search books by author: {author}.");
                BooksSearchRequest searchRequest = new BooksSearchRequest(author, SearchOption.ByAuthor);
                BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for book by anything.
        /// </summary>
        /// <param name="anything">Any search term.</param>
        /// <remarks>Uses Google Books API to search for book by anything.</remarks>
        /// <returns>Book matched by anything.</returns>
        /// <response code="200">Search result was found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Book/Anything/{anything}")]
        [SwaggerResponse(HttpStatusCode.OK, "Book returned by search.", typeof(BookSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBookByAnythingAsync(string anything)
        {
            try
            {
                await logger.LogInfoAsync($"Search book by anything: {anything}.");
                BookSearchRequest searchRequest = new BookSearchRequest(anything, SearchOption.ByAnything);
                BookSearchResponse searchResponse = await bookSearchService.SearchBookAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for books by anything.
        /// </summary>
        /// <param name="anything">Any search term.</param>
        /// <remarks>Uses Google Books API to search for books by anything.</remarks>
        /// <returns>Books matched by anything.</returns>
        /// <response code="200">Search results were found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Books/Anything/{anything}")]
        [SwaggerResponse(HttpStatusCode.OK, "Books returned by search.", typeof(BooksSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBooksByAnythingAsync(string anything)
        {
            try
            {
                await logger.LogInfoAsync($"Search books by anything: {anything}.");
                BooksSearchRequest searchRequest = new BooksSearchRequest(anything, SearchOption.ByAnything);
                BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for book by isbn.
        /// </summary>
        /// <param name="isbn">ISBN of book.</param>
        /// <remarks>Uses Google Books API to search for book by ISBN.</remarks>
        /// <returns>Book matched by ISBN.</returns>
        /// <response code="200">Search result was found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Book/Isbn/{isbn}")]
        [SwaggerResponse(HttpStatusCode.OK, "Book returned by search.", typeof(BookSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBookByIsbnAsync(string isbn)
        {
            try
            {
                await logger.LogInfoAsync($"Search book by ISBN: {isbn}.");
                BookSearchRequest searchRequest = new BookSearchRequest(isbn, SearchOption.ByIsbn);
                BookSearchResponse searchResponse = await bookSearchService.SearchBookAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for books by isbn.
        /// </summary>
        /// <param name="isbn">ISBN of books.</param>
        /// <remarks>Uses Google Books API to search for books by ISBN.</remarks>
        /// <returns>Books matched by ISBN.</returns>
        /// <response code="200">Search results were found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Books/Isbn/{isbn}")]
        [SwaggerResponse(HttpStatusCode.OK, "Books returned by search.", typeof(BooksSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBooksByIsbnAsync(string isbn)
        {
            try
            {
                await logger.LogInfoAsync($"Search books by ISBN: {isbn}.");
                BooksSearchRequest searchRequest = new BooksSearchRequest(isbn, SearchOption.ByIsbn);
                BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for book by title.
        /// </summary>
        /// <param name="title">Title of book.</param>
        /// <remarks>Uses Google Books API to search for book by title.</remarks>
        /// <returns>Book matched by title.</returns>
        /// <response code="200">Search result was found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Book/Title/{title}")]
        [SwaggerResponse(HttpStatusCode.OK, "Book returned by search.", typeof(BookSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBookByTitleAsync(string title)
        {
            try
            {
                await logger.LogInfoAsync($"Search book by title: {title}.");
                BookSearchRequest searchRequest = new BookSearchRequest(title, SearchOption.ByTitle);
                BookSearchResponse searchResponse = await bookSearchService.SearchBookAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for books by title.
        /// </summary>
        /// <param name="title">Title of books.</param>
        /// <remarks>Uses Google Books API to search for books by title.</remarks>
        /// <returns>Books matched by title.</returns>
        /// <response code="200">Search results were found.</response>
        /// <response code="500">Error occurred during search.</response>
        [HttpGet]
        [Route("Books/Title/{title}")]
        [SwaggerResponse(HttpStatusCode.OK, "Books returned by search.", typeof(BooksSearchResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResult))]
        public async Task<IHttpActionResult> SearchBooksByTitleAsync(string title)
        {
            try
            {
                await logger.LogInfoAsync($"Search books by title: {title}.");
                BooksSearchRequest searchRequest = new BooksSearchRequest(title, SearchOption.ByTitle);
                BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync(ex);
                return InternalServerError(ex);
            }
        }
    }
}