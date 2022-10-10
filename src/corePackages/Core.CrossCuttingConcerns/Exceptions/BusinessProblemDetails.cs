using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public bool IsSuccess { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
