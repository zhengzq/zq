using System;
using System.Collections.Generic;
using System.Linq;
using Example.Web.Core.Domain.Navigations;
using Zq;
using Zq.Ioc;

namespace Example.Web.Core.Query
{
    public interface IRoleQueryService : IQueryService
    {
        //List<NavigationRecord> GetModulesByRoleId(int roleId);
    }
    [Component(typeof(IRoleQueryService))]
    public class RoleQueryService : IRoleQueryService
    {
        //public List<NavigationRecord> GetModulesByRoleId(int roleId)
        //{
        //    var role = _roleRepository.Get(roleId);
        //    var rolePermissions = GetRolePermissions(roleId);

        //    var records = StandardNavigationProvider.Instance.GetNavigationRecords();

        //    Func<NavigationRecord, bool> expression = x =>
        //    {
        //        return rolePermissions.Exists(y => y.Type == PermissionTypeEnum.导航
        //        && y.NavicationCode == x.Code
        //        && y.Domain == role.Domain);
        //    };

        //    var module = records.Where(x => string.IsNullOrWhiteSpace(x.ParentId)).Where(expression).OrderBy(x => x.Order).ToList();

        //    module.ForEach(x =>
        //    {
        //        var list = records.Where(y => y.ParentCode == x.Code).Where(expression).ToList();
        //        x.Childs.AddRange(list);
        //    });
        //    return module;
        //}
        //public List<dynamic> GetRolePermissions(int roleId)
        //{
        //    var permissions = new List<dynamic>();

        //    return permissions;
        //}
    }
}
