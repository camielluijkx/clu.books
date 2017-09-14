using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using clu.books.library.Mapping;
using clu.books.library.search;
using clu.books.library.settings;
using Swashbuckle.Swagger.Annotations;
using model = clu.books.library.model;
using dto = clu.books.library.dto;

namespace clu.books.web.api.Controllers
{
    [AllowAnonymous]
    [BooksModelBinding]
    [RoutePrefix("Search")]
    public class BookSearchController : ApiController
    {
        private readonly IConfigurationSettings configurationSettings;

        private readonly IBookSearchService bookSearchService;

        public BookSearchController()
        {
            configurationSettings = new ConfigurationSettings();
            bookSearchService = new BookSearchService(configurationSettings);
        }

        /// <summary>
        /// Collection of books returned by search.
        /// </summary>
        public class BooksSearchResponse
        {
            public string SearchTerm { get; set; }

            public dto.Books Books { get; set; }

            public BooksSearchResponse(string searchTerm, dto.Books books)
            {
                SearchTerm = searchTerm;
                Books = books;
            }
        }

        /// <summary>
        /// Book returned by search.
        /// </summary>
        public class BookSearchResponse
        {
            public string SearchTerm { get; set; }

            public dto.Book Book { get; set; }

            public BookSearchResponse(string searchTerm, dto.Book book)
            {
                SearchTerm = searchTerm;
                Book = book;
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
                model.Books books = await bookSearchService.SearchBooksByAuthorAsync(author);

                BooksSearchResponse searchResponse = new BooksSearchResponse(author, books.ToDto());

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
                model.Books books = await bookSearchService.SearchBooksByAnythingAsync(anything);

                BooksSearchResponse searchResponse = new BooksSearchResponse(anything, books.ToDto());

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
                model.Book book = await bookSearchService.SearchBookByIsbnAsync(isbn);

                BookSearchResponse searchResponse = new BookSearchResponse(isbn, book.ToDto());

                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}