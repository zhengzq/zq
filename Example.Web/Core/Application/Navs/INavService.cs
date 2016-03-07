using Zq;

namespace Example.Web.Core.Application.Navs
{
    public interface INavService
    {
        OperateResult Create(CreateNavCommand command);
        OperateResult Update(UpdateNavCommand command);
        OperateResult Delete(DeleteNavCommand command);

        OperateResult InitNavigationFromAssembly(string assemblyName);
    }
}
