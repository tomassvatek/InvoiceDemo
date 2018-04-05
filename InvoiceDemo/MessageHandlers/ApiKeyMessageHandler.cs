using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceDemo.MessageHandlers
{
    public class ApiKeyMessageHandler : DelegatingHandler
    {
        private const string ApiKey = "349h588jfjagsf";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;
            bool checkApiKeyExists = request.Headers.TryGetValues("Key", out requestHeaders);

            if (checkApiKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(ApiKey))
                {
                    validKey = true;
                }
            }

            if (!validKey)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}