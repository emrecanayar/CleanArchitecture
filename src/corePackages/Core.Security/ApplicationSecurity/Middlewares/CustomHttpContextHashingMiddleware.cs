using Core.Security.Hashing;
using Core.Security.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Core.Security.ApplicationSecurity.Middlewares
{
    public class CustomHttpContextHashingMiddleware
    {
        private readonly RequestDelegate _next;
        private Stopwatch _stopwatch;
        private readonly CustomHttpContextHashing _securityKey;

        public CustomHttpContextHashingMiddleware(RequestDelegate next, IOptions<CustomHttpContextHashing> securityKey)
        {
            _next = next;
            _securityKey = securityKey.Value;
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
                //Logging information will be added later
                Console.WriteLine(exception);
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

                //_logger.LogDebug($"RequestLog:{DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(0, 10000)}-{api.ElapsedTime}ms", $"{JsonConvert.SerializeObject(api)}");
                return Task.CompletedTask;
            });

            //_logger.LogInformation($"Finished handling request.{_stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
