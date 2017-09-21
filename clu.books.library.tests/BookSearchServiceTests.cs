using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using clu.books.library.search;
using clu.books.library.settings;
using clu.books.library.Search;

namespace clu.books.library.tests
{
    [TestClass]
    public class BookSearchServiceTests
    {
        private Mock<IConfigurationSettings> configurationSettingsMock;

        private IBookSearchMapper bookSearchMapper;

        private BookSearchService objectUnderTest;

        [TestInitialize]
        public void Initialize()
        {
            configurationSettingsMock = new Mock<IConfigurationSettings>();
            configurationSettingsMock
                .Setup(p => p.GoogleBooksPublicApiKey)
                .Returns("AIzaSyClbYvehfHHdnnwU1ifh8Wj9qECEsXx0Hs");
            configurationSettingsMock
                .Setup(p => p.OrderSearchResultsBy)
                .Returns("Relevance");
            configurationSettingsMock
                .Setup(p => p.MaxSearchResults)
                .Returns(40);
            configurationSettingsMock
                .Setup(p => p.PreferredLanguage)
                .Returns("EN");

            bookSearchMapper = new BookSearchMapper();
            bookSearchMapper.Configure();

            objectUnderTest = new BookSearchService(configurationSettingsMock.Object, bookSearchMapper);
        }

        [TestMethod]
        public async Task SearchBookByIsbn_ExistingIsbnNumber_ReturnsBestMatch()
        {
            string isbn = "9781876175702";
            BookSearchRequest searchRequest = new BookSearchRequest(isbn, SearchOption.ByIsbn);
            BookSearchResponse searchResponse = await objectUnderTest.SearchBookAsync(searchRequest);

            Assert.IsNotNull(searchResponse.Book);
            Assert.AreEqual("1) The motorcycle diaries - Ernesto Guevara - 2003 (EN)", searchResponse.Book.Information);
        }

        [TestMethod]
        public async Task SearchBooksByAuthor_ExistingAuthor_ReturnsMultipleBooks()
        {
            string isbn = "Stephen King";
            BooksSearchRequest searchRequest = new BooksSearchRequest(isbn, SearchOption.ByAuthor);
            BooksSearchResponse searchResponse = await objectUnderTest.SearchBooksAsync(searchRequest);

            Assert.IsNotNull(searchResponse.Books);
            Assert.IsTrue(searchResponse.Books.Count > 1);
        }
    }
}
