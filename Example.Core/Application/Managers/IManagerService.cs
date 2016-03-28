using System.Collections.Generic;
using Zq;

namespace Example.Core.Application.Managers
{
    public interface IManagerService 
    {
        OperateResult Update(UpdateManagerCommand command);
        OperateResult Create(CreateManagerCommand command);
        OperateResult Delete(List<int> managerIds);
        OperateResult UpdatePassword(UpdatePasswordCommand command);
        bool CheckLoginName(string loginName);
        OperateResult GetManagerByLoginName(string loginName);
        bool ValidateLoginNameWithPassword(string loginName, string password);
    }
}
