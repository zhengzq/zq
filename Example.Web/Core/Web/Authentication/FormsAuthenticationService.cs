using System;
using System.Web;
using System.Web.Security;
using Zq.Ioc;

namespace Example.Web.Core.Web.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    [Component(typeof(IAuthenticationService),LifeTime.Hierarchical)]
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;

        private readonly TimeSpan _expirationTimeSpan;

        private CurrentUser _cachedUser;
        private readonly string _sessionKey = "USER";

        public FormsAuthenticationService(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
            this._cachedUser = HttpContext.Current.Session[_sessionKey] as CurrentUser;
        }


        public virtual void SignIn(CurrentUser user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.UserId.ToString(),
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.ToString(),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);

            _cachedUser = user;

            HttpContext.Current.Session[_sessionKey] = user;
        }

        public virtual void SignOut()
        {

            HttpContext.Current.Session[_sessionKey] = null;
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public virtual CurrentUser GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;

            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null)
            {
                _cachedUser = user;
                HttpContext.Current.Session.Remove(_sessionKey);
                HttpContext.Current.Session.Add(_sessionKey, user);
            }
            return _cachedUser;
        }

        public virtual CurrentUser GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var json = ticket.UserData;

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return CurrentUser.FromJson(json);
        }
    }
}