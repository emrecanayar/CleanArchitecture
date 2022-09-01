using Core.Integration.Common;
using Core.Integration.Dto;
using Core.Integration.Serialization;
using Core.Integration.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration.Clients
{
    public class JsonPlaceHolderClient
    {
        public PostStore Post { get; set; }
        public ApiSession ApiSession { get; }

        public JsonPlaceHolderClient(string username, string password, ApiEnvironment environment, string userAgent = "") :
          this(username, password, ApiUrlFromEnvironment(environment), userAgent)
        { }

        public JsonPlaceHolderClient(ApiCredentials credentials, ApiEnvironment environment, string userAgent = "") :
        this(new ApiSession
        {
            ApiUrl = ApiUrlFromEnvironment(environment),
            UserAgent = userAgent,
            Credentials = credentials
        })
        { }

        public JsonPlaceHolderClient(string username, string password, string apiUrl, string userAgent = "") :
         this(new ApiSession
         {
             ApiUrl = apiUrl,
             UserAgent = userAgent,
             Credentials = new ApiCredentials
             {
                 Username = username,
                 Password = password
             }
         })
        { }

        public JsonPlaceHolderClient(ApiCredentials credentials, string apiUrl, string userAgent = "") :
            this(new ApiSession { ApiUrl = apiUrl, UserAgent = userAgent, Credentials = credentials })
        { }

        public JsonPlaceHolderClient(ApiSession apiSession) : this(apiSession, new JsonSerializer()) { }

        public JsonPlaceHolderClient(ApiSession apiSession, IJsonSerializer jsonSerializer)
        {
            if (string.IsNullOrEmpty(apiSession.UserAgent))
            {
                apiSession.UserAgent = GetDefaultUserAgent();
            }
            Post = new PostStore(apiSession, jsonSerializer);
            ApiSession = apiSession;
        }


        public static string ApiUrlFromEnvironment(ApiEnvironment environment)
        {
            switch (environment)
            {
                case ApiEnvironment.Dev: return Constants.JsonPlaceHolderBaseUrl;
                case ApiEnvironment.Prod: return Constants.JsonPlaceHolderBaseUrl;
                default: return string.Empty;
            }
        }

        public static string GetDefaultUserAgent()
        {
            return $"JsonPlaceHolder .NET SDK/{Constants.Version} (+JsonPlaceHolder)";
        }
    }


}
