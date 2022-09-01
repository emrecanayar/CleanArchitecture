using Core.Integration.Common;
using Core.Integration.Dto;
using Core.Integration.Models.JsonPlaceHolder;
using Core.Integration.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration.Stores
{
    public class PostStore : BaseStore
    {
        public PostStore(ApiSession apiSession, IJsonSerializer jsonSerializer) : base(apiSession, ApiControllers.JsonPlaceHolder, jsonSerializer) { }

        public async Task<Post> GetPost(string postId)
        {
            var url = ApiUrlHelper.GetApiUrlForController(ApiSession.ApiUrl, ApiControllerUri, postId);
            return await Get<Post>(url).ConfigureAwait(false);
        }
    }
}
