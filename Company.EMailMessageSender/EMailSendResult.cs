using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.EMailMessageSender
{
    /// <summary>
    /// Represents email message send result
    /// </summary>
    public class EMailSendResult
    {
        /// <summary>
        /// If True send operation completed successfully
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        /// <summary>
        /// If operation completed successfully contains ids of sent messages
        /// </summary>
        [JsonProperty("message_ids")]
        public List<string> MessageIds { get; set; }
        /// <summary>
        /// If operation completed with errors contains errors.
        /// </summary>
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
