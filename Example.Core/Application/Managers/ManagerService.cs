using System.Collections.Generic;
using Example.Core.Domain.Managers;
using Zq;
using Zq.Domain;
using Zq.Ioc;
using Zq.UnitOfWork;

namespace Example.Core.Application.Managers
{
    [Component(typeof(IManagerService), LifeTime.Hierarchical)]
    public class ManagerService : IManagerService
    {

        private readonly IUnitOfWork _unitWork;
        private readonly IManagerRepository _managerRepository;
        public ManagerService(IUnitOfWork unitWork
            , IManagerRepository managerRepository)
        {
            _unitWork = unitWork;
            _managerRepository = managerRepository;
        }

        public OperateResult Update(UpdateManagerCommand command)
        {
            try
            {
                var data = _managerRepository.Get(command.ManagerId);
                data.Cellphone = command.Cellphone;
                data.Email = command.Email;
                data.RoleId = command.RoleId;
                data.UserName = command.UserName;
                //不填写则不修改密码
                if (!string.IsNullOrWhiteSpace(command.Password))
                    data.Password = command.Password;

                data.SetIsEnable(command.IsEnable);

                _managerRepository.Update(data);

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);

               
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult Create(CreateManagerCommand command)
        {
            try
            {
                if (!command.RepeatPassword.Equals(command.Password))
                    throw new DomainException("两次密码输入不正确", 100);

                if (!_managerRepository.CheckLoginName(command.LoginName))
                    throw new DomainException("登录名已经存在", 100);

                var data = new Manager(0
                    , command.LoginName
                    , command.UserName
                    , command.Password
                    , command.RoleId
                    , command.Cellphone
                    , command.Email);

                data.SetIsEnable(command.IsEnable);

                _managerRepository.Add(data);

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult Delete(List<int> managerIds)
        {
            try
            {
                managerIds.ForEach(id =>
                {
                    var data = _managerRepository.Get(id);
                    data.CheckIsSys();
                    _managerRepository.Delete(data);
                });

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public OperateResult UpdatePassword(UpdatePasswordCommand command)
        {
            try
            {
                var data = _managerRepository.Get(command.ManagerId);
                data.UpdatePassword(command.Password, command.RepeatPassword);
                _managerRepository.Update(data);

                _unitWork.Commit();

                return new OperateResult(ResultState.Success);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }
        }

        public bool CheckLoginName(string loginName)
        {
            return _managerRepository.CheckLoginName(loginName);
        }

        public OperateResult GetManagerByLoginName(string loginName)
        {
            try
            {
                var manager = _managerRepository.GetManagerByLoginName(loginName);

                manager.CheckIsEnable();
                return new OperateResult(ResultState.Success, "", 0, manager);
            }
            catch (DomainException dex)
            {
                return new OperateResult(ResultState.Error, dex.Message, dex.Code);
            }

        }

        public bool ValidateLoginNameWithPassword(string loginName, string password)
        {
            return _managerRepository.Validate(loginName, password);
        }
    }
}