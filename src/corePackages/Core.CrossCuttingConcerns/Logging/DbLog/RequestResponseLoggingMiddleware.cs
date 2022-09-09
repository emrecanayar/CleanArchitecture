using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public class RequestResponseLoggingMiddleware
    {
        private const string DEFAULT_ERROR_MESSAGE = "An error occured, please contact with team.";
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, RecyclableMemoryStreamManager recyclableMemoryStreamManager)
        {
            _next = next;
            this.recyclableMemoryStreamManager = recyclableMemoryStreamManager;
        }


    }
}
