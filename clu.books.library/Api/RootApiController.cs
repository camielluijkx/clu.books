using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace clu.books.library.api
{
    public abstract class RootApiController : ApiController
    {
        private readonly Assembly exposingAssembly;

        protected RootApiController()
        {
            exposingAssembly = Assembly.GetCallingAssembly();
        }

        protected RootApiController(Assembly exposingAssembly)
        {
            this.exposingAssembly = exposingAssembly;
        }

        public virtual HttpResponseMessage Get()
        {
            string apiName = exposingAssembly.GetName().Name;
            string environmentName = ConfigurationManager.AppSettings["EnvironmentName"] ?? "???";
            string releaseNumber = ConfigurationManager.AppSettings["ReleaseNumber"] ?? "???";
            string buildnumber = exposingAssembly.GetName().Version.ToString();

            string version = string.Format("{3} {0} - Build {1} - Release: {2} ", environmentName, buildnumber, releaseNumber, apiName);

            return Request.CreateResponse(HttpStatusCode.OK, version);
        }
    }
}
