using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpBinClient : AbstractRestClient
    {
        public HttpBinClient(HttpClient httpClient, RestServiceSettings settings, ILogger logger) : base(httpClient, settings, logger)
        {
        }
    }
}
