using System;
using System.Collections.Generic;
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
        private ControllerTestClient testClient;

        [TestInitialize]
        public void Initialize()
        {
            testClient = new ControllerTestClient();
        }

        [TestMethod]
        public async Task SearchBooksByAnything_ResultsFound_ReturnsOk()
        {
            // Arrange
            string anything = DateTime.UtcNow.Ticks.ToString();

            // Act
            BooksSearchResponse searchResponse = await testClient.GetAsync<BooksSearchResponse>($"http://localhost/clu.books.web.api/Search/Books/Anything/{anything}");

            // Assert
            Assert.IsNotNull(searchResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "The remote server returned an error: (429).")]
        public async Task SearchBooksByAnything_ThrottlesRequests_ReturnsTooManyRequests()
        {
            // Arrange
            string anything = "Test";

            // Act
            List<Task<BooksSearchResponse>> requestTasks = new List<Task<BooksSearchResponse>>();
            for (int i = 0; i < 20; i++)
            {
                Task<BooksSearchResponse> requestAction = testClient.GetAsync<BooksSearchResponse>($"http://localhost/clu.books.web.api/Search/Books/Anything/{anything}");
                requestTasks.Add(requestAction);
            }
            await Task.WhenAll(requestTasks);
        }
    }
}
