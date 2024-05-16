using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.EMailMessageSender;
using Newtonsoft.Json;
using System;

namespace EMailMessageSenderTests
{
    [TestClass]
    public class EMailMessageTests
    {
        [TestMethod]
        public void SerializeEMailMessageTest()
        {
            var from = new EMailAddress(Consts.SOURCE_EMAIL, Consts.SOURCE_EMAIL_OWNER);
            var to = new EMailAddress(Consts.DESTINATION_EMAIL, Consts.DESTINATION_EMAIL_OWNER);
            var message = new EMailMessage() { From = from, Subject="Test" };
            message.To.Add(to);
            var serialized = JsonConvert.SerializeObject(message);
            var deserialized = JsonConvert.DeserializeObject<EMailMessage>(serialized);

        }
    }
}
