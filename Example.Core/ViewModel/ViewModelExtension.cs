using AutoMapper;
using Example.Core.Query.Dto;

namespace Example.Core.ViewModel
{
    public static class ViewModelExtension
    {
        public static ManagerDto To(this ManagerModel model)
        {
            return Mapper.Map<ManagerDto>(model);
        }
    }
}