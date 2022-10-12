﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class AuthorizationProblemDetails : ProblemDetailsExtend
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
