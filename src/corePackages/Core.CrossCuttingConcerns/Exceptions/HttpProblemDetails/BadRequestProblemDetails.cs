using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class BadRequestProblemDetails : ProblemDetailsExtend
    {
        public BadRequestProblemDetails(string detail)
        {
            Title = "Bad Request violation";
            Detail = detail;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/business";
            Instance = "";
            IsSuccess = false;
        }
    }
}
