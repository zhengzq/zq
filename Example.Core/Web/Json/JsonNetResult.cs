using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Example.Core.Web.Json
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }

        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
                {
                    //这句是解决问题的关键,也就是json.net官方给出的解决配置选项.//循环引用依赖                 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if ((this.JsonRequestBehavior == System.Web.Mvc.JsonRequestBehavior.DenyGet)
                &&
                string.Equals(context.HttpContext.Request.HttpMethod, 
                "GET", 
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("NotAllowed");
            }
            var response = context.HttpContext.Response;

            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                response.Write(JsonConvert.SerializeObject(this.Data, Settings));
            }
        }

    }
}