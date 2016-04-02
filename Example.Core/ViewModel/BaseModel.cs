using AutoMapper;

namespace Example.Core.ViewModel
{
    public class BaseModel
    {
        public T ToDto<T>()
        {
           return Mapper.Map<T>(this);
        }
    }
}