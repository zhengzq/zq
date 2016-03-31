using System.Collections.Generic;
using Example.Core.Domain.Roles;
using Zq;
using Zq.DI;
using Zq.Domain;
using Zq.UnitOfWork;

namespace Example.Core.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitWork;
        public RoleService(IUnitOfWork unitWork
            , IRoleRepository roleRepository)
        {
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
                role.ClearPermission();

                command.Permissions.ForEach(x =>
                {
                    if (x.Item2)
                    {
                        role.SetPermission(x.Item1);
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
                        role.SetPermission(x.Item1);
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

        public OperateResult Delete(List<int> roleIds)
        {
            try
            {
                roleIds.ForEach(x =>
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

        public bool CheckRoleIsAuthorized(string permissionId, int roleId)
        {
            var role = _roleRepository.Get(roleId);
            return role.RolePermissions.Exists(x => x.PermissionId == permissionId);
        }

    }
}