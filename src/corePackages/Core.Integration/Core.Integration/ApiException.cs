using System.Net;

namespace Core.Integration
{
    public class ApiException : WebException
    {
        public HttpStatusCode StatusCode { get; internal set; }

        public ErrorMessage ErrorMessage { get; internal set; }

        public ApiException(string message, HttpStatusCode statusCode, ErrorMessage errorMessage, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
