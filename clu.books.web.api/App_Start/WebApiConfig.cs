using System.Net.Http.Formatting;
using System.Web.Http;
using clu.books.library.Ioc;
using Microsoft.Practices.Unity;
using WebApiThrottle;

namespace clu.books.web.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();

            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Configure request throttling, for options see https://github.com/stefanprodan/WebApiThrottle.
            config.MessageHandlers.Add(new ThrottlingHandler
            {
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 20, perHour: 200, perDay: 1500, perWeek: 3000)
                {
                    IpThrottling = true
                },
                Repository = new CacheRepository()
            });
        }
    }
}
