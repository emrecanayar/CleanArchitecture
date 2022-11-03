using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Core.Application.Pipelines.Limits
{
    public class RequestLimitBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public string Name
        {
            get;
        }

        public int NoOfRequest
        {
            get;
            set;
        } = 1;

        public int Seconds
        {
            get;
            set;
        } = 1;

        private static MemoryCache Cache
        {
            get;
        } = new MemoryCache(new MemoryCacheOptions());

        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestLimitBehavior(IHttpContextAccessor httpContextAccessor, string name)
        {
            _httpContextAccessor = httpContextAccessor;
            Name = name;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var memoryCacheKey = $"{Name}-{ipAddress}";
            Cache.TryGetValue(memoryCacheKey, out int prevReqCount);
            if (prevReqCount >= NoOfRequest)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                throw new BusinessException($"Request limit is exceeded. Try again in {Seconds} seconds.");
            }
            else
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(memoryCacheKey, (prevReqCount + 1), cacheEntryOptions);
            }

            return await next();
        }
    }
}