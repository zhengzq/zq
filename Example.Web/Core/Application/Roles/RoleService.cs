using Example.Web.Core.Domain.Navigations;
using Example.Web.Core.Domain.Permissions;
using Example.Web.Core.Domain.Roles;
using Zq;
using Zq.Domain;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Application.Roles
{
    [Component(typeof(IRoleService))]
    public class RoleService :  IRoleService
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitWork;
        public RoleService(IUnitOfWork unitWork
            , INavigationRepository navigationRepository
            , IPermissionRepository permissionRepository
            , IRoleRepository roleRepository)
        {
            _navigationRepository = navigationRepository;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _unitWork = unitWork;
        }

        public OperateResult Update(UpdateRoleCommand command)
        {
            try
            {
                var role = _roleRepository.Get(command.Id);
                role.Name = command.Name;
                role.Order = command.Order;
                role.ClearPermission();
                command.Permissions.ForEach(x =>
                {
                    if (x.Item2)
                    {
                        var permission = _permissionRepository.Get(x.Item1);
                        if (permission != null)
                            role.SetPermission(permission.Id, permission.Code);
                    }

                });
                _roleRepository.Update(role);
                _unitWork.Commit();
                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult Create(CreateRoleCommand command)
        {
            try
            {
                var role = new Role(command.Name
                    , command.Order);

                command.Permissions.ForEach(x =>
                {
                    if (x.Item2)
                    {
                        var permission = _permissionRepository.Get(x.Item1);
                        if (permission != null)
                            role.SetPermission(permission.Id, permission.Code);
                    }
                });
                _roleRepository.Add(role);
                _unitWork.Commit();
                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult Delete(DeleteRoleCommand command)
        {
            try
            {
                command.RoleIds.ForEach(x =>
                {
                    var role = _roleRepository.Get(x);
                    _roleRepository.Delete(role);
                });
                _unitWork.Commit();
                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }
    }
}