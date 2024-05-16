using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.EMailMessageSender
{
    /// <summary>
    /// Represents message attachment
    /// </summary>
    public class EMailMessageAttachment
    {
        /// <summary>
        /// Attachment content
        /// </summary>
        [JsonRequired]
        [JsonProperty("content")]
        public string Content { get; set; }
        /// <summary>
        /// Attachment type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Attachment file name
        /// </summary>
        [JsonRequired]
        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }
        [JsonProperty("content_id")]
        public string ContentId { get; set; }
    }
}
