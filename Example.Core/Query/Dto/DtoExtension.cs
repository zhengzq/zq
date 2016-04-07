using AutoMapper;
using Example.Core.ViewModel;

namespace Example.Core.Query.Dto
{
    public static class DtoExtension
    {
        public static ManagerModel ToModel(this ManagerDto dto)
        {
            return Mapper.Map<ManagerModel>(dto);
        }
        public static RoleModel ToModel(this RoleDto dto)
        {
            return Mapper.Map<RoleModel>(dto);
        }
    }
}
