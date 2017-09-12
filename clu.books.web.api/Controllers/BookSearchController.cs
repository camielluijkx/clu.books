using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using clu.books.library.Mapping;
using clu.books.library.search;
using clu.books.library.settings;
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

        [HttpGet]
        [Route("Author/{author}")]
        [ResponseType(typeof(dto.Books))]
        public async Task<IHttpActionResult> SearchBooksByAuthorAsync(string author)
        {
            model.Books books = await bookSearchService.SearchBooksByAuthorAsync(author);

            return Ok(books.ToDto());
        }

        [HttpGet]
        [Route("Anything/{anything}")]
        [ResponseType(typeof(dto.Books))]
        public async Task<IHttpActionResult> SearchBooksByAnythingAsync(string anything)
        {
            model.Books books = await bookSearchService.SearchBooksByAnythingAsync(anything);

            return Ok(books.ToDto());
        }

        [HttpGet]
        [Route("Isbn/{isbn}")]
        [ResponseType(typeof(dto.Book))]
        public async Task<IHttpActionResult> SearchBookByIsbnAsync(string isbn)
        {
           model.Book book = await bookSearchService.SearchBookByIsbnAsync(isbn);

            return Ok(book.ToDto());
        }
    }
}