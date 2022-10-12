using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class NotFoundProblemDetails : ProblemDetailsExtend
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
