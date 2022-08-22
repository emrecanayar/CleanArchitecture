using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Security.Hashing;
using Core.Security.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Core.Security.ApplicationSecurity.Middlewares
{
    public class CustomHttpContextHashingMiddleware
    {
        private readonly RequestDelegate _next;
        private Stopwatch _stopwatch;
        private readonly CustomHttpContextHashing _securityKey;
        private readonly LoggerServiceBase _loggerServiceBase;

        public CustomHttpContextHashingMiddleware(RequestDelegate next, IOptions<CustomHttpContextHashing> securityKey, LoggerServiceBase loggerServiceBase)
        {
            _next = next;
            _securityKey = securityKey.Value;
            _loggerServiceBase = loggerServiceBase;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            var api = new ApiRequestInput
            {
                HttpType = context.Request.Method,
                Query = context.Request.QueryString.Value,
                RequestUrl = context.Request.Path,
                RequestName = "",
                RequestIP = context.Request.Host.Value
            };

            var request = context.Request.Body;
            var response = context.Response.Body;

            try
            {
                using (var newRequest = new MemoryStream())
                {
                    context.Request.Body = newRequest;

                    using (var newResponse = new MemoryStream())
                    {
                        context.Response.Body = newResponse;

                        using (var reader = new StreamReader(request))
                        {
                            api.Body = await reader.ReadToEndAsync();
                            if (string.IsNullOrEmpty(api.Body))
                                await _next.Invoke(context);

                            api.Body = HashingHelper.AESDecrypt(api.Body, _securityKey.SecurityKey);
                        }
                        using (var writer = new StreamWriter(newRequest))
                        {
                            await writer.WriteAsync(api.Body);
                            await writer.FlushAsync();
                            newRequest.Position = 0;
                            context.Request.Body = newRequest;
                            await _next(context);
                        }

                        using (var reader = new StreamReader(newResponse))
                        {
                            newResponse.Position = 0;
                            api.ResponseBody = await reader.ReadToEndAsync();
                            if (!string.IsNullOrWhiteSpace(api.ResponseBody))
                            {
                                api.ResponseBody = HashingHelper.AESEncrypt(api.ResponseBody, _securityKey.SecurityKey);
                            }
                        }
                        using (var writer = new StreamWriter(response))
                        {
                            await writer.WriteAsync(api.ResponseBody);
                            await writer.FlushAsync();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _loggerServiceBase.Error($"An error occurred in the Hashing Middleware : {exception.Message}");
            }
            finally
            {
                context.Request.Body = request;
                context.Response.Body = response;
            }

            context.Response.OnCompleted(() =>
            {
                _stopwatch.Stop();
                api.ElapsedTime = _stopwatch.ElapsedMilliseconds;

                _loggerServiceBase.Debug($"RequestLog:{DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(0, 10000)}-{api.ElapsedTime}ms =>  {JsonConvert.SerializeObject(api)}");

                return Task.CompletedTask;
            });

            _loggerServiceBase.Info($"Finished handling request.{_stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
