using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Company.EMailMessageSender.Mailtrap
{
    /// <summary>
    /// Represents IEMailMessageSender implementation for MailtrapAPI v2.0
    /// </summary>
    public class MailtrapEMailMessageSender : IEMailMessageSender
    {
        private readonly RestClient apiEndpoint;
        private readonly Dictionary<string, string> headers;

        /// <summary>
        /// Initializes new instance of sender
        /// </summary>
        /// <param name="apiBaseUrl">Mailtrap email sending API URL</param>
        /// <param name="apiToken">Mailtrap API token</param>
        public MailtrapEMailMessageSender(string apiBaseUrl, string apiToken) 
        {
            var options = new RestClientOptions(apiBaseUrl)
            {
                ThrowOnAnyError = false
            };
            apiEndpoint = new RestClient(options);
            headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {apiToken}" },
                { "Content-Type", "application/json" }
            };
        }

        public EMailSendResult SendMessage(EMailMessage message)
        {
            if (null == message) throw new ArgumentNullException(nameof(message));

            return Send(message);
        }

        private EMailSendResult Send(EMailMessage message) 
        {
            RestRequest request = new RestRequest();
            foreach (var header in headers) 
            {
                request.AddHeader(header.Key, header.Value);
            }
            request.RequestFormat = DataFormat.Json;
            request.Method = Method.Post;
            request.AddBody(JsonConvert.SerializeObject(message));
            var response = apiEndpoint.Execute(request);
            var result = JsonConvert.DeserializeObject<EMailSendResult>(response.Content);
            return result;
        }
    }
}
