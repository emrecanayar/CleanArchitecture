using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Integration.Dto
{
    public class ApiSession
    {
        public string UserAgent { get; set; }
        public string ApiUrl { get; set; }
        public ApiCredentials Credentials { get; set; }
        public WebProxy Proxy { get; set; }
    }
}
