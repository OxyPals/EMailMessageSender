using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Company.EMailMessageSender
{
    /// <summary>
    /// Represents email message
    /// </summary>
    public class EMailMessage
    {
        /// <summary>
        /// Sender address
        /// </summary>
        [JsonProperty("from")]
        [JsonRequired]
        public EMailAddress From { get; set; }

        /// <summary>
        /// List of recipients
        /// </summary>
        [JsonProperty("to")]
        [JsonRequired]
        public List<EMailAddress> To { get; set; }

        /// <summary>
        /// Message subject
        /// </summary>
        [JsonProperty("subject")]
        [JsonRequired]
        public string Subject { get; set; }

        /// <summary>
        /// Plain message text
        /// </summary>
        [JsonProperty("text")]
        [JsonRequired]
        public string Text { get; set; }

        /// <summary>
        /// Message text in html format
        /// </summary>
        [JsonProperty("html", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Html { get; set; }

        /// <summary>
        /// List of attachments
        /// </summary>
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
