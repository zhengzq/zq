using Zq;

namespace Example.Web.Core.Application.Permissions
{
    public interface IPermissionService
    {
        OperateResult InitPermissionFromAssembly(string assemblyName);
        /// <summary>
        /// 当前管理员是否有权限-Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        bool Authorize(string code, int managerId);
    }
}
