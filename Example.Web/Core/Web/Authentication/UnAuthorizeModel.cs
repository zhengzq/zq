namespace Example.Web.Core.Web.Authentication
{
    public class UnAuthorizeModel
    {
        public UnAuthorizeModel(string message, string controller, string action, string username)
        {
            this.Message = message;
            this.Controller = controller;
            this.Action = action;
            this.UserName = username;
        }

        public string Message { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public string UserName { get; private set; }
    }
}