using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.EMailMessageSender
{
    /// <summary>
    /// Interface for implementation of EMail message senders
    /// </summary>
    public interface IEMailMessageSender
    {
        /// <summary>
        /// Sends message
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <returns>Send operation result</returns>
        EMailSendResult SendMessage(EMailMessage message);
    }
}
