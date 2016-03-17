using System;
using System.Collections.Generic;
using System.Linq;
using Example.Web.Core.Domain.Navigations;
using Zq;
using Zq.Ioc;
using Zq.Paging;

namespace Example.Web.Core.Query
{
    public interface IManagerQueryService : IQueryService
    {
        IPagedList<dynamic> Find(int index, int size, ManagerOption option);
    }
    [Component(typeof(IManagerQueryService))]
    public class ManagerQueryService : IManagerQueryService
    {
        public IPagedList<dynamic> Find(int index, int size, ManagerOption option)
        {
            return new PagedList<dynamic>(new List<dynamic>()
            {
                new {LoginName="zzq",UserName="郑志强",CellPhone=110,RoleName="超级管理员",Id=1,Cellphone="1"},
                new {LoginName="zzq1",UserName="郑志强1",CellPhone=110,RoleName="超级管理员",Id=2,Cellphone="2"},
                new {LoginName="zzq2",UserName="郑志强2",CellPhone=110,RoleName="超级管理员",Id=3,Cellphone="3"},
            }, index, size);
        }
    }
}
