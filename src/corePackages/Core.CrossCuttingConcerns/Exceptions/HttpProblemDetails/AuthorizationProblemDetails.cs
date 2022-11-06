using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class AuthorizationProblemDetails : ProblemDetailsExtend
    {
        public AuthorizationProblemDetails(string detail)
        {
            Title = "Authorization error";
            Detail = detail;
            Status = StatusCodes.Status401Unauthorized;
            Type = "https://example.com/probs/authorization";
            Instance = "";
            IsSuccess = false;
        }
    }
}
