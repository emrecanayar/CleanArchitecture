using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public bool IsSuccess { get; set; }
        public object Errors { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
