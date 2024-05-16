using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.EMailMessageSender
{
    public class EMailSendResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("message_ids")]
        public List<string> MessageIds { get; set; }
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
