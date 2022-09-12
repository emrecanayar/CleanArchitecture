using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Core.CrossCuttingConcerns.Logging.DbLog.Dto
{
    public class LogDto
    {
        public ObjectId Id { get; set; }
        public string EventId { get; set; }
        public string LogDomain { get; set; }
        public string UserId { get; set; }
        public DateTime LogDate { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string Scheme { get; set; }
        public string QueryString { get; set; }
        public string RemoteIp { get; set; }
        public string Headers { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string Exception { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerException { get; set; }
        public string InnerExceptionMessage { get; set; }
        [JsonIgnore]
        public string GetLog => $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{Scheme} " +
                                   $"Host: {Host} " +
                                   $"Path: {Path} " +
                                   $"QueryString: {QueryString} " +
                                   $"UserID:{UserId}" +
                                   $"Remote Ip:{RemoteIp}" +
                                   $"Headers:{Headers}" +
                                   $"Request Body: {RequestBody}" +
                                   $"Response Body: {ResponseBody}";
        [JsonIgnore]
        public string GetErrorLog => $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{Scheme} " +
                                   $"Host: {Host} " +
                                   $"Path: {Path} " +
                                   $"QueryString: {QueryString} " +
                                   $"UserID:{UserId}" +
                                   $"Remote Ip:{RemoteIp}" +
                                   $"Headers:{Headers}" +
                                   $"Request Body: {RequestBody}" +
                                   $"Error : {Exception}";
    }
}
