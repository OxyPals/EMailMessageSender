using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.EMailMessageSender;
using Company.EMailMessageSender.Mailtrap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace EMailMessageSenderTests
{
    [TestClass]
    public class EMailTests
    {
        private const string ApiBaseUrl = "https://send.api.mailtrap.io/api/send";
        private const string ApiToken = "ea93e0f65f1aaa91c355f6b15a2aea57";
        private static readonly EMailAddress validFromWithName = new EMailAddress("mailtrap@demomailtrap.com", "Tester");
        private static readonly EMailAddress validToWithName = new EMailAddress("oxysoft2015@gmail.com", "Max");

        [TestMethod]
        public void SendTextTest_Success()
        {
            IEMailMessageSender sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            EMail.RegisterSender("mailtrap", sender, useAsDeafult:true);

            var result = EMail.SendText(validFromWithName, "EMail subject", "EMail text", null, validToWithName);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendHtmlTest_Success()
        {
            IEMailMessageSender sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            EMail.RegisterSender("mailtrap", sender, useAsDeafult: true);

            var result = EMail.SendHtml(validFromWithName, "EMail subject", @"<html><body>Email html</body></html>", null, validToWithName);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }
        [TestMethod]
        public void SendTextAndHtmlTest_Success()
        {
            IEMailMessageSender sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            EMail.RegisterSender("mailtrap", sender, useAsDeafult: true);

            var result = EMail.Send(validFromWithName, "EMail subject", "EMail text", @"<html><body>Email html</body></html>", null, validToWithName);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendMessageTest_Success()
        {
            IEMailMessageSender sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            EMail.RegisterSender("mailtrap", sender, useAsDeafult: true);
            
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "EMail subject", Text = "EMail text", Html = @"<html><body>Email html</body></html>" };

            var result = EMail.Send(testMessage);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        // This block of tests specifies current EMail message contract based on Mailtrap API spec

        [TestMethod]
        public void ValidateMessage_EmptyFrom() 
        {
            var testMessage = new EMailMessage() { To = new List<EMailAddress> { validToWithName }, Subject = "EMail subject", Text = "EMail text" };
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void ValidateMessage_EmptyTo()
        {
            var testMessage = new EMailMessage() { From = validFromWithName, Subject = "EMail subject", Text = "EMail text" };
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void ValidateMessage_ToCountInRange() 
        {
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress>(), Subject = "EMail subject", Text = "EMail text" };
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void ValidateMessage_SubjectRequired() 
        {
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress>(), Text = "EMail text" };
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void ValidateMessage_TextRequired()
        {
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress>(), Subject = "EMail subject" };
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void ValidateMessage_AttachmentContentRequired() 
        {
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName}, Subject = "EMail subject", Text = "EMail text" };
            testMessage.Attachments.Add(new EMailMessageAttachment { FileName = "Test" });
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void ValidateMessage_AttachmentFileNameRequired()
        {
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "EMail subject", Text = "EMail text" };
            var content = Encoding.UTF8.GetBytes("Attachment content");

            testMessage.Attachments.Add(new EMailMessageAttachment { Content = Convert.ToBase64String(content) });
            var result = EMail.ValidateMessage(testMessage);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Count > 0);
        }
    }
}
