using Zq;

namespace Example.Web.Core.Application.Managers
{
    public interface IManagerService 
    {
        OperateResult Update(UpdateManagerCommand command);
        OperateResult Create(CreateManagerCommand command);
        OperateResult Delete(DeleteManagerCommand command);
        OperateResult UpdatePassword(UpdatePasswordCommand command);
        bool CheckLoginName(string loginName);
    }
}
