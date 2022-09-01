using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration.Serialization
{
    public interface IJsonSerializer
    {
        string Serialize(object item);

        T Deserialize<T>(string json);
    }
}
