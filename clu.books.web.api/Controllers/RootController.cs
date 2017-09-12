using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using clu.books.library.api;

namespace clu.books.web.api.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("")]
    [ExcludeFromCodeCoverage]
    public class RootController : RootApiController
    {
        public RootController()
            : base(Assembly.GetExecutingAssembly())
        {

        }

        [Route("")]
        [HttpGet]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }
    }
}