using System;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using Newtonsoft.Json;

namespace clu.books.web.api
{
    public class BooksModelBinding : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            MediaTypeFormatterCollection formatters = controllerSettings.Formatters;

            formatters.Remove(formatters.JsonFormatter);
            formatters.Add(new JsonMediaTypeFormatter());
            JsonFormatterHelper.SetJsonFormatterDefaults(formatters);

            JsonMediaTypeFormatter jsonFormatter = formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
        }
    }
}