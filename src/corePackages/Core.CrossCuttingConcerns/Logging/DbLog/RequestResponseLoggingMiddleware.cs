using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Logging.DbLog.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System.Security.Claims;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Logging.DbLog
{
    public class RequestResponseLoggingMiddleware
    {
        private const string DEFAULT_ERROR_MESSAGE = "An error occured, please contact with team.";
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context, ILogService logService)
        {
            LogDto log = new LogDto();
            log.LogDate = DateTime.Now;
            log = await LogRequest(context, log);

            Claim userClaim = context.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userClaim != null)
            {
                log.UserId = userClaim.Value;
            }

            log = await LogRequest(context, log);
            log = await LogResponse(context, log);
            _ = logService.CreateLog(log);

        }

        private async Task<LogDto> LogRequest(HttpContext context, LogDto log)
        {
            try
            {
                context.Request.EnableBuffering();
                using var requestStream = _recyclableMemoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStream);
                log.RequestBody = ReadStreamInChunks(requestStream);
                log.Path = context.Request.Path;
                log.Host = context.Request.Host.Value;
                log.QueryString = context.Request.QueryString.ToString();
                log.RemoteIp = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                log.Headers = string.Join(",", context.Request.Headers.Select(he => he.Key + ":[" + he.Value + "]").ToList());
                context.Request.Body.Position = 0;
            }
            catch (Exception exception)
            {

                log.Exception = JsonSerializer.Serialize(exception);
                log.ExceptionMessage = JsonSerializer.Serialize(exception.Message);
                if (exception.InnerException != null)
                {
                    log.InnerException = JsonSerializer.Serialize(exception.InnerException);
                    log.InnerExceptionMessage = JsonSerializer.Serialize(exception.InnerException.Message);
                }
            }

            return log;
        }

        private async Task<LogDto> LogResponse(HttpContext context, LogDto log)
        {

            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                List<string> errors = new List<string>();
                log.Exception = JsonSerializer.Serialize(exception);
                errors.Add(exception.ToString());
                log.ExceptionMessage = JsonSerializer.Serialize(exception.Message);
                errors.Add(GetErrorMessage(exception));
                if (exception.InnerException != null)
                {
                    log.InnerException = JsonSerializer.Serialize(exception.InnerException);
                    errors.Add(exception.InnerException.ToString());
                    log.InnerExceptionMessage = JsonSerializer.Serialize(exception.InnerException.Message);
                    errors.Add(GetErrorMessage(exception.InnerException));
                }

                context.Response.ContentType = "application/json";

                switch (exception)
                {
                    case BusinessException b:
                        throw new BusinessException(b.Message);
                    case NotFoundException n:
                        throw new NotFoundException(n.Message);
                    case ValidationException v:
                        throw new ValidationException(v.Message);
                    case AuthorizationException a:
                        throw new AuthorizationException(a.Message);
                    default:
                        throw new BusinessException(exception.Message);

                }
                var response = "";
                string jsonString = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonString);
            }
            finally
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                log.ResponseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            return log;
        }

        private string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        public string GetErrorMessage(Exception exception)
        {
            return exception.Message;
        }
    }
}
