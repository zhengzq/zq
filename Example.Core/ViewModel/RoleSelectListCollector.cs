using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Example.Core.Query;
using Example.Core.Web.Extensions;
using Zq.DI;

namespace Example.Core.ViewModel
{
    public class RoleSelectListCollector : SelectListDataCollector
    {
        public override List<SelectListItem> Execute()
        {
            var dtos = Ioc.Resolve<IRoleQueryService>().Find();

            return dtos.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
        }
    }
}
