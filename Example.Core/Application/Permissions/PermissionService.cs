using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Example.Core.Domain.Permissions;
using Zq.DI;
using Zq.UnitOfWork;

namespace Example.Core.Application.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitWork;

        public PermissionService(IPermissionRepository permissionRepository
            , IUnitOfWork unitWork)
        {
            _permissionRepository = permissionRepository;
            _unitWork = unitWork;
        }

        public void InitNavigationPermissionWithFunctionPermissionFromAssembly(List<string> navigationIds, string assemblyName)
        {
            var existPermissions = _permissionRepository.GetAllPermission();
            var a = Assembly.Load(assemblyName);
            var types = a.GetTypes();
            var ts = types.Where(x => x.BaseType != null && x.BaseType.Name.Equals("AdminApiController"));
            //初始化Action权限
            foreach (var type in ts)
            {
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                foreach (var method in methods)
                {
                    var attrs = method.GetCustomAttributes(typeof(PermissionDescriptionAttribute));
                    foreach (var attribute in attrs)
                    {
                        var r = attribute as PermissionDescriptionAttribute;
                        if (r == null)
                            break;
                        if (string.IsNullOrWhiteSpace(r.PermissionId))
                            break;

                        if (existPermissions.Exists(x => x.Id == r.PermissionId))
                            break;

                        var permission = new Permission(r.PermissionId
                            , r.Name
                            , r.NavigationId
                            , r.Order
                            , r.IsEnable
                            , r.Type);

                        _permissionRepository.Add(permission);
                    }
                }
            }
            //初始化导航权限
            foreach (var navigationId in navigationIds)
            {
                var id = $"{navigationId}View";

                if (existPermissions.Exists(x => x.Id == id))
                    continue;

                var permission = new Permission()
                {
                    Id = id,
                    Name = "查看",
                    NavigationId = navigationId,
                    Order = 0,
                    Type = PermissionType.导航
                };

                _permissionRepository.Add(permission);
            }
            _unitWork.Commit();
        }


        public Permission GetPermissionById(string permissionId)
        {
           return _permissionRepository.Get(permissionId);
        }
    }
}