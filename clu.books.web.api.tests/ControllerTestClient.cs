using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace clu.books.web.api.tests
{
    internal enum Verb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    internal class ControllerTestClient
    {
        internal string EndPoint { get; set; }

        internal Verb Method { get; set; }

        internal string ContentType { get; set; }

        internal string PostData { get; set; }

        internal ControllerTestClient()
        {
            EndPoint = "";
            Method = Verb.GET;
            ContentType = "application/JSON";
            PostData = "";
        }

        internal ControllerTestClient(string endpoint, Verb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/json";
            PostData = postData;
        }

        internal async Task<string> RequestAsync()
        {
            return await RequestAsync("");
        }

        internal async Task<string> RequestAsync(string parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = $"Faile: Received HTTP {response.StatusCode}";
                    throw new ApplicationException(message);
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                        }
                    }
                }

                return responseValue;
            }
        }
    }
}
