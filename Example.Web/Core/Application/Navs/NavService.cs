using System.Linq;
using System.Reflection;
using Example.Web.Core.Domain.Navigations;
using Example.Web.Core.Domain.Permissions;
using Zq;
using Zq.Domain;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Web.Core.Application.Navs
{
    [Component(typeof(INavService))]
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
                if (_navigationRepository.CheckExsitSystemName(command.SystemName))
                    throw new DomainException(string.Format("调用名称 {0} 已经存在", command.SystemName));

                var nav =new Navigation(command.SystemName
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
                    Code = string.Format("{0}.show", nav.SystemName),
                    IsEnable = true,
                    Name = "显示(Show)",
                    NavSystemName = nav.SystemName,
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
                var nav = _navigationRepository.Get(command.Id);
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

        public OperateResult Delete(DeleteNavCommand command)
        {
            try
            {
                command.NavIds.ForEach(id =>
                {
                    var nav = _navigationRepository.Get(id);
                    nav.CheckIsSys();

                    if (_navigationRepository.CheckHaveChildNode(nav))
                        throw new DomainException(string.Format("请先删除 {0} 的子节点", nav.Name));

                    _navigationRepository.Delete(nav);

                    var permissions = _permissionRepository.GetPermissionsByNavSystemName(nav.SystemName);
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

        public OperateResult InitNavigationFromAssembly(string assemblyName)
        {
            try
            {
                var existNavigations = _navigationRepository.GetAllNavigation();
                var a = Assembly.Load(assemblyName);
                var types = a.GetTypes();
                var ts = types.Where(x => x.BaseType != null && x.BaseType.Name.Equals("BaseApiController"));
                foreach (var type in ts)
                {
                    var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                    foreach (var method in methods)
                    {
                        var attrs = method.GetCustomAttributes(typeof(NavigationDescriptionAttribute));
                        foreach (var attribute in attrs)
                        {
                            var r = attribute as NavigationDescriptionAttribute;
                            if (r == null)
                                break;

                            if (existNavigations.Exists(x => x.SystemName == r.SystemName))
                                break;

                            var nav = new Navigation(r.SystemName
                                , 0
                                , 0
                                , r.IsHide
                                , r.FullName
                                , r.Icon
                                , r.Link
                                , r.Remark
                                , r.IsSys);

                            _navigationRepository.Add(nav);
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
    }
}