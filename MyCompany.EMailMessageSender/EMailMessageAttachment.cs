using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.EMailMessageSender
{
    public class EMailMessageAttachment
    {
        [JsonRequired]
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonRequired]
        [JsonProperty("filename")]
        public string FileName { get; set; }
        [JsonProperty("disposition")]
        public string Disposition { get; set; }
        [JsonProperty("content_id")]
        public string ContentId { get; set; }
    }
}
