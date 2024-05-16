using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.EMailMessageSender
{
    public class EMailMessage
    {
        [JsonProperty("from")]
        [JsonRequired]
        public EMailAddress From { get; set; }

        [JsonProperty("to")]
        [JsonRequired]
        public List<EMailAddress> To { get; set; }

        [JsonProperty("subject")]
        [JsonRequired]
        public string Subject { get; set; }

        [JsonProperty("text")]
        [JsonRequired]
        public string Text { get; set; }

        [JsonProperty("html", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Html { get; set; }

        [JsonProperty("attachments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<EMailMessageAttachment> Attachments { get; set; }
        public EMailMessage() 
        {
            To = new List<EMailAddress>();
            Attachments = new List<EMailMessageAttachment> { };
        }

        public bool ShouldSerializeAttachments()
        {
            return (Attachments.Count > 0);
        }
    }
}
