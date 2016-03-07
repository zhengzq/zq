using System.Collections.Generic;

namespace Example.Web.Core.Application.Navs
{
    public class DeleteNavCommand
    {
        public DeleteNavCommand(List<int> navIds)
        {
            this.NavIds = navIds;
        }

        public List<int> NavIds { get; set; }
    }
}
