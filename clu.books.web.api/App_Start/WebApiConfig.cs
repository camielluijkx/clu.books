using System.Net.Http.Formatting;
using System.Web.Http;
using clu.books.library.Ioc;
using clu.books.library.search;
using clu.books.library.settings;
using clu.books.library.Search;
using Microsoft.Practices.Unity;

namespace clu.books.web.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IConfigurationSettings, ConfigurationSettings>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookSearchService, BookSearchService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookSearchMapper, BookSearchMapper>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
