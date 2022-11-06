using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class NotFoundProblemDetails : ProblemDetailsExtend
    {
        public NotFoundProblemDetails(string detail)
        {
            Title = "Not found";
            Detail = detail;
            Status = StatusCodes.Status404NotFound;
            Type = "https://example.com/probs/notfound";
            Instance = "";
            IsSuccess = false;
        }
    }
}
