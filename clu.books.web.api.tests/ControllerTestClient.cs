using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace clu.books.web.api.tests
{
    internal class ControllerTestClient
    {
        public async Task<T> GetAsync<T>(string url)
        {
            T responseValue = default(T);

            HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                string message = $"Failed: Received HTTP {responseMessage.StatusCode}";
                throw new ApplicationException(message);
            }

            responseValue = await responseMessage.Content.ReadAsAsync<T>();

            return responseValue;
        }
    }
}
