using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ProblemDetailsExtend : ProblemDetails
    {
        public bool IsSuccess { get; set; }
    }
}
