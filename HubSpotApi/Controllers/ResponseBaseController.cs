using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpotApi.Controllers
{
    class ResponseBaseController
    {
        public string ApiKey { get; set; }
        public string BaseURL { get; set; }

        public ResponseBaseController(string apiKey, string baseURL)
        {
            ApiKey = apiKey;
            BaseURL = baseURL;
        }
    }
}
