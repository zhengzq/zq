using System.Web.Mvc;
using Zq.DI;
using Zq.Serializers;

namespace Example.Core.Web
{
    public class Binder<T> : IModelBinder where T : class, new()
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var filter = request.Form["filter"];
            var start = string.IsNullOrEmpty(request.Form["start"]) ? 0 : int.Parse(request.Form["start"]);
            var length = string.IsNullOrEmpty(request.Form["length"]) ? 10 : int.Parse(request.Form["length"]);

            var search = new PageOption<T>()
            {
                Index = (start / length) + 1,
                Size = length,
            };

            if (!string.IsNullOrEmpty(filter))
            {
                var serialize = Ioc.Resolve<IJsonSerializer>();
                var option = serialize.Deserialize(filter, bindingContext.ModelType) as T;
                search.Option = option;
            }

            return search;
        }
    }
}