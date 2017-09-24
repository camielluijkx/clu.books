using System;
using System.Net;
using System.Threading.Tasks;
using clu.books.library.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace clu.books.web.api.tests
{
    [TestClass]
    public class BookSearchControllerTests
    {
        [TestMethod]
        public async Task SearchBooksByAnything_ResultsFound_ReturnsOk()
        {
            // Arrange
            ControllerTestClient testClient = new ControllerTestClient
            {
                EndPoint = @"http://localhost/clu.books.web.api",
                Method = Verb.GET
            };

            // Act
            long ticks = DateTime.UtcNow.Ticks;
            string responseValue = await testClient.RequestAsync($"/Search/Books/Anything/test");
            BooksSearchResponse searchResponse = JsonConvert.DeserializeObject<BooksSearchResponse>(responseValue);

            // Assert
            Assert.IsNotNull(searchResponse.Books);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException), "The remote server returned an error: (429).")]
        public async Task SearchBooksByAnything_ThrottlesRequests_ReturnsTooManyRequests()
        {
            // Arrange
            ControllerTestClient testClient = new ControllerTestClient
            {
                EndPoint = @"http://localhost/clu.books.web.api",
                Method = Verb.GET
            };

            // Act
            for (int i = 0; i < 20; i++)
            {
                await testClient.RequestAsync("/Search/Books/Anything/Test");
            }
        }
    }
}
