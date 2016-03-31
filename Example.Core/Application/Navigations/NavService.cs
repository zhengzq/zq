using System.Collections.Generic;
using System.Linq;
using Example.Core.Domain.Navigations;
using Example.Core.Domain.Permissions;
using Zq;
using Zq.DI;
using Zq.Domain;
using Zq.UnitOfWork;

namespace Example.Core.Application.Navigations
{
    public class NavService : INavService
    {
        private readonly INavigationRepository _navigationRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitWork;
        public NavService(IUnitOfWork unitWork
            , INavigationRepository navigationRepository
            , IPermissionRepository permissionRepository)
        {
            _navigationRepository = navigationRepository;
            _permissionRepository = permissionRepository;
            _unitWork = unitWork;
        }

        public OperateResult Create(CreateNavCommand command)
        {
            try
            {
                if (_navigationRepository.CheckExsitId(command.NavigationId))
                    throw new DomainException(string.Format("调用名称 {0} 已经存在", command.NavigationId));

                var nav =new Navigation(command.NavigationId
                , command.ParentId
                , command.Order
                , command.IsHide
                , command.Name
                , command.Icon
                , command.Link
                , command.Remark);

                _navigationRepository.Add(nav);

                var permission = new Permission()
                {
                    Id = string.Format("{0}.show", nav.Id),
                    IsEnable = true,
                    Name = "显示(Show)",
                    NavigationId = nav.Id,
                    Order = 1,
                    Type = PermissionType.导航
                };
                _permissionRepository.Add(permission);

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult Update(UpdateNavCommand command)
        {
            try
            {
                var nav = _navigationRepository.Get(command.NavigationId);
                nav.ParentId = command.ParentId;
                nav.Order = command.Order;
                nav.IsHide = command.IsHide;
                nav.Name = command.Name;
                nav.Icon = command.Icon;
                nav.Link = command.Link;
                nav.Remark = command.Remark;
                _navigationRepository.Update(nav);

                if (command.Permissions != null)
                {
                    command.Permissions.ForEach(x =>
                    {
                        var permission = _permissionRepository.Get(x.Item1);
                        permission.IsEnable = x.Item2;
                        _permissionRepository.Update(permission);
                    });
                }

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult Delete(List<string> navigationIds)
        {
            try
            {
                navigationIds.ForEach(id =>
                {
                    var nav = _navigationRepository.Get(id);
                    nav.CheckIsSys();

                    if (_navigationRepository.CheckHaveChildNode(nav))
                        throw new DomainException(string.Format("请先删除 {0} 的子节点", nav.Name));

                    _navigationRepository.Delete(nav);

                    var permissions = _permissionRepository.GetPermissionsByNavigationId(nav.Id);
                    if (permissions != null)
                        permissions.ForEach(x => _permissionRepository.Delete(x));
                });

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public List<NavigationRecord> GetModules()
        {
            var list = GetNavigationRecords().ToList();
            var m = list.Where(x => string.IsNullOrEmpty(x.ParentId)).OrderBy(x => x.Order).ToList();
            m.ForEach(x =>
            {
                list.ForEach(y =>
                {
                    if (y.ParentId == x.NavigationId)
                        x.Childs.Add(y);
                });
            });
            return m;
        }

        public List<NavigationRecord> GetNavigationRecords()
        {
            var provider = new StandardNavigationProvider();
            return provider.GetNavigationRecords().ToList();
        }
    }
}