using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyCompany.EMailMessageSender
{
    public static class EMail
    {
        private static readonly Dictionary<string, IEMailMessageSender> senders;
        private static IEMailMessageSender Default;

        static EMail() 
        {
            senders = new Dictionary<string, IEMailMessageSender>();
        }

        public static void RegisterSender(string senderName, IEMailMessageSender sender, bool useAsDeafult = false) 
        {
            if (string.IsNullOrWhiteSpace(senderName)) throw new ArgumentNullException(nameof(senderName));
            if (sender == null) throw new ArgumentNullException(nameof(sender));

            senders[senderName] = sender;
            if (useAsDeafult) Default = sender;
        }

        public static EMailSendResult SendText(EMailAddress from, string subject, string text, IEnumerable<EMailMessageAttachment> attachments = null, params EMailAddress[] to) 
        {
            if (null == Default) throw new InvalidOperationException("You need to set default message sender.");

            var message = new EMailMessage() { From = from, Subject = subject, Text = text, To = new List<EMailAddress>(to) };
            if (null != attachments) 
            {
                message.Attachments = new List<EMailMessageAttachment>(attachments);
            }
            var validationResult = ValidateMessage(message);
            if (!validationResult.Success) 
            {
                return new EMailSendResult() { Success = false, Errors = validationResult.Errors };
            }
            try 
            {
                return Default.SendMessage(message);
            }catch(Exception ex) 
            {
                return new EMailSendResult() { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public static EMailSendResult SendHtml(EMailAddress from, string subject, string html, IEnumerable<EMailMessageAttachment> attachments = null, params EMailAddress[] to) 
        {
            if (null == Default) throw new InvalidOperationException("You need to set default message sender.");

            var message = new EMailMessage() { From = from, Subject = subject, Text = " ", Html = html, To = new List<EMailAddress>(to) };
            if (null != attachments)
            {
                message.Attachments = new List<EMailMessageAttachment>(attachments);
            }
            var validationResult = ValidateMessage(message);
            if (!validationResult.Success)
            {
                return new EMailSendResult() { Success = false, Errors = validationResult.Errors };
            }
            try
            {
                return Default.SendMessage(message);
            }
            catch (Exception ex)
            {
                return new EMailSendResult() { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public static EMailSendResult Send(EMailAddress from, string subject, string text, string html, IEnumerable<EMailMessageAttachment> attachments = null, params EMailAddress[] to)
        {
            if (null == Default) throw new InvalidOperationException("You need to set default message sender.");

            var message = new EMailMessage() { From = from, Subject = subject, Text = text, Html = html, To = new List<EMailAddress>(to) };
            if (null != attachments)
            {
                message.Attachments = new List<EMailMessageAttachment>(attachments);
            }
            var validationResult = ValidateMessage(message);
            if (!validationResult.Success)
            {
                return new EMailSendResult() { Success = false, Errors = validationResult.Errors };
            }
            try
            {
                return Default.SendMessage(message);
            }
            catch (Exception ex)
            {
                return new EMailSendResult() { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public static EMailSendResult Send(EMailMessage message) 
        {
            if (null == Default) throw new InvalidOperationException("You need to set default message sender.");

            var validationResult = ValidateMessage(message);
            if (!validationResult.Success)
            {
                return new EMailSendResult() { Success = false, Errors = validationResult.Errors };
            }
            try
            {
                return Default.SendMessage(message);
            }
            catch (Exception ex)
            {
                return new EMailSendResult() { Success = false, Errors = new List<string> { ex.Message } };
            }
        }

        public static EMailValidationResult ValidateMessage(EMailMessage message)
        {
            var resultsAsExceptions = new List<Exception>();
            if (null == message) resultsAsExceptions.Add(new ArgumentNullException(nameof(message)));
            if (null == message.From) resultsAsExceptions.Add(new ArgumentNullException(nameof(message.From)));
            if (null == message.To) resultsAsExceptions.Add(new ArgumentNullException(nameof(message.To)));
            if (message.To.Count <= 0 || message.To.Count > 1000) resultsAsExceptions.Add(new ArgumentOutOfRangeException(nameof(message.To), "Recipient count must be >= 1 and <=1000 "));
            if (string.IsNullOrEmpty(message.Subject)) resultsAsExceptions.Add(new ArgumentNullException(nameof(message.Subject)));
            if (string.IsNullOrEmpty(message.Text)) resultsAsExceptions.Add(new ArgumentNullException(nameof(message.Text)));
            if (null != message.Attachments && message.Attachments.Count > 0)
            {
                foreach (var attachment in message.Attachments)
                {
                    if (string.IsNullOrEmpty(attachment.Content)) resultsAsExceptions.Add(new ArgumentNullException(nameof(attachment.Content)));
                    if (string.IsNullOrEmpty(attachment.FileName)) resultsAsExceptions.Add(new ArgumentNullException(nameof(attachment.FileName)));
                }
            }
            if (resultsAsExceptions.Count > 0)
                return EMailValidationResult.Fail(new AggregateException(resultsAsExceptions));
            else
                return EMailValidationResult.Ok();
        }
    }
}
