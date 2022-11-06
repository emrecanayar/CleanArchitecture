using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class ProblemDetailsExtend : ProblemDetails
    {
        public bool IsSuccess { get; set; }
    }
}
