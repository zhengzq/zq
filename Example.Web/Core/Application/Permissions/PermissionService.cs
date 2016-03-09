using System.Linq;
using System.Reflection;
using Example.Web.Core.Domain.Managers;
using Example.Web.Core.Domain.Permissions;
using Example.Web.Core.Domain.Roles;
using Zq;
using Zq.Domain;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Application.Permissions
{
    [ComponentAttribute(typeof(IPermissionService))]
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitWork;
        public PermissionService(IPermissionRepository permissionRepository
            , IUnitOfWork unitWork, IManagerRepository managerRepository
            , IRoleRepository roleRepository)
        {
            _permissionRepository = permissionRepository;
            _unitWork = unitWork;
            _managerRepository = managerRepository;
            _roleRepository = roleRepository;
        }

        public OperateResult InitPermissionFromAssembly(string assemblyName)
        {
            try
            {
                var existPermissions = _permissionRepository.GetAllPermission();
                var a = System.Reflection.Assembly.Load(assemblyName);
                var types = a.GetTypes();
                var ts = types.Where(x => x.BaseType != null && x.BaseType.Name.Equals("AdminApiController"));
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
                            if (string.IsNullOrWhiteSpace(r.Code))
                                break;

                            if (existPermissions.Exists(x => x.Code == r.Code))
                                break;

                            var permission = new Permission(r.Code
                                , r.Name
                                , r.NavSystemName
                                , r.Order
                                , r.IsEnable
                                , r.Type);

                            _permissionRepository.Add(permission);
                        }
                    }
                }
                _unitWork.Commit();
                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public bool Authorize(string code, int managerId)
        {
            var manager = _managerRepository.Get(managerId);
            if (manager.IsSys) return true;
            var role = _roleRepository.Get(manager.RoleId);
            return role.RolePermissions.Exists(x => x.PermissionCode == code);
        }
    }
}