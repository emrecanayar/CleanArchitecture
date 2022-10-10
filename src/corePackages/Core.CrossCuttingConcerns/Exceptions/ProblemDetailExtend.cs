using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ProblemDetailExtend : ProblemDetails
    {
        public bool IsSuccess { get; set; }
    }
}
