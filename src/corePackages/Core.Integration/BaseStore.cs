using Core.Integration.Dto;
using Core.Integration.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration
{
    public abstract class BaseStore : BaseRestClient
    {
        protected readonly string ApiControllerUri;
        public BaseStore(ApiSession apiSession, string apiControllerUri, IJsonSerializer jsonSerializer) : base(apiSession, jsonSerializer)
        {
            ApiControllerUri = apiControllerUri;
        }
    }
}
