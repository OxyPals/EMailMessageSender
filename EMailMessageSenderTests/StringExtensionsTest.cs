using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.EMailMessageSender;
using System;

namespace EMailMessageSenderTests
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void ValidEMailTest()
        {
            Assert.IsTrue(Consts.VALID_EMAIL.IsEMail());
        }

        [TestMethod]
        public void InvalidEMailTest() 
        {
            Assert.IsFalse(Consts.INVALID_EMAIL.IsEMail());
        }
    }
}
