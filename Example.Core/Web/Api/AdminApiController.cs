using System.Web.Http;
using Example.Core.Web.Authentication;

namespace Example.Core.Web.Api
{
    [ApiMonitor]
    [ApiException]
    [System.Web.Mvc.Authorize]
    public class AdminApiController : ApiController
    {
        public CurrentUser CurrentUser
        {
            get { return User.Identity as CurrentUser; }
        }

    }
}
