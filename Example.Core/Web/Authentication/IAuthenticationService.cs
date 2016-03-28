
namespace Example.Core.Web.Authentication
{
   
    public partial interface IAuthenticationService 
    {
        void SignIn(CurrentUser user, bool createPersistentCookie);
        void SignOut();
        CurrentUser GetAuthenticatedUser();
    }
}