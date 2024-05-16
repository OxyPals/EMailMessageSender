using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.EMailMessageSender;
using Company.EMailMessageSender.Mailtrap;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMailMessageSenderTests
{
    [TestClass]
    public class MailtrapEMailMessageSenderTests
    {
        private const string ApiBaseUrl = "https://send.api.mailtrap.io/api/send";
        private const string ApiToken = "ea93e0f65f1aaa91c355f6b15a2aea57";
        private static readonly EMailAddress validFromWithName = new EMailAddress("mailtrap@demomailtrap.com", "Tester");
        private static readonly EMailAddress validFromWithoutName = new EMailAddress("mailtrap@demomailtrap.com");
        private static readonly EMailAddress validToWithName = new EMailAddress("oxysoft2015@gmail.com", "Max");
        private static readonly EMailAddress validToWithoutName = new EMailAddress("oxysoft2015@gmail.com");

        private static readonly EMailAddress invalidFromWithName = new EMailAddress("mailtrap@gmail.com", "Tester");
        private static readonly EMailAddress invalidToWithName = new EMailAddress("oxysoft2016@gmail.com", "Max");

        private static readonly string Whitespace = "";

        [TestMethod]
        public void SendTextEMailWithAddressOwnerNameWithoutAttachmentsTest_Success()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "Test subject", Text = "Test text message" };
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendTextEMailWithoutAddressOwnerNameWithoutAttachmentsTest_Success()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithoutName, To = new List<EMailAddress> { validToWithoutName }, Subject = "Test subject", Text = "Test text message" };
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendTextEMailWithAddressOwnerNameWithoutAttachmentsTest_FailFrom()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = invalidFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "Test subject", Text = "Test text message" };
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(!result.Success);
            Assert.IsNotNull(result.Errors);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void SendTextEMailWithAddressOwnerNameWithoutAttachmentsTest_FailTo()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { invalidToWithName }, Subject = "Test subject", Text = "Test text message" };
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(!result.Success);
            Assert.IsNotNull(result.Errors);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod]
        public void SendHtmlEMailWithAddressOwnerNameWithoutAttachmentsTest_Success()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "Test subject", Text = Whitespace, Html = @"<html><body>Test Html</body></html>" };
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendTextHtmlEMailWithAddressOwnerNameWithoutAttachmentsTest_Success()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "Test subject", Text = "Test text", Html = @"<html><body>Test Html</body></html>" };
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendTextEMailWithAddressOwnerNameWithAttachmentsTest_Success()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "Test subject", Text = "Test text message" };
            var content = Encoding.UTF8.GetBytes("Attachment content");
            testMessage.Attachments.Add(new EMailMessageAttachment() { FileName = "RestShrap.xml", Content = Convert.ToBase64String(content) });
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.MessageIds);
            Assert.IsTrue(result.MessageIds.Count > 0);
        }

        [TestMethod]
        public void SendTextEMailWithAddressOwnerNameWithAttachmentsTest_Fail()
        {
            var sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            var testMessage = new EMailMessage() { From = validFromWithName, To = new List<EMailAddress> { validToWithName }, Subject = "Test subject", Text = "Test text message" };
            testMessage.Attachments.Add(new EMailMessageAttachment() { FileName = "RestShrap.xml", Content = "Attachment" });
            var result = sender.SendMessage(testMessage);
            Assert.IsTrue(!result.Success);
            Assert.IsNotNull(result.Errors);
            Assert.IsTrue(result.Errors.Count > 0);
        }
    }
}
