using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace clu.books.web.api
{
    public static class JsonFormatterHelper
    {
        public static void SetJsonFormatterDefaults(MediaTypeFormatterCollection formatters)
        {
            JsonMediaTypeFormatter jsonFormatter = formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}