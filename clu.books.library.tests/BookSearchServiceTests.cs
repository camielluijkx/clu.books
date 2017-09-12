using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace clu.books.library.tests
{
    [TestClass]
    public class BookSearchServiceTests
    {
        private Mock<IConfigurationSettings> configurationSettingsMock;

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

            objectUnderTest = new BookSearchService(configurationSettingsMock.Object);
        }

        [TestMethod]
        public async Task SearchBookByIsbn_ExistingIsbnNumber_ReturnsVolume()
        {
            string isbn = "9781876175702";
            Book book = await objectUnderTest.SearchBookByIsbnAsync(isbn);

            Assert.IsNotNull(book);
            Assert.AreEqual("1) The motorcycle diaries - Ernesto Guevara - 2003 (EN)", book.ToString());
        }
    }
}
