using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zq.Serializers
{
    public partial interface IJsonSerializer
    {
        T Deserialize<T>(string json) ;

        string Serialize(object obj);
    }
}
