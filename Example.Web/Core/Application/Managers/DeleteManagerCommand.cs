using System.Collections.Generic;

namespace Example.Web.Core.Application.Managers
{
    public class DeleteManagerCommand
    {
        public DeleteManagerCommand(List<int> managerIds)
        {
            this.ManagerIds = managerIds;
           
        }
        public List<int> ManagerIds { get; set; }
   
    }
}
