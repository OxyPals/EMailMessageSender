using Microsoft.VisualStudio.TestTools.UnitTesting;
using Company.EMailMessageSender;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailMessageSenderTests
{
    [TestClass]
    public class EMailAddressTests
    {
        [TestMethod]
        public void CreateValidEMailAddressTest() 
        {
            var testAddress = new EMailAddress(Consts.SOURCE_EMAIL, Consts.SOURCE_EMAIL_OWNER);
            Assert.IsNotNull(testAddress);
            Assert.AreEqual(Consts.SOURCE_EMAIL, testAddress.EMail);
            Assert.AreEqual(Consts.SOURCE_EMAIL_OWNER, testAddress.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateEMailAddressWithEmptyEMailTest() 
        {
            var testAdrress = new EMailAddress("", Consts.SOURCE_EMAIL_OWNER);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEMailAddressWithInvalidEMailTest() 
        {
            var testAddress = new EMailAddress(Consts.INVALID_EMAIL, Consts.SOURCE_EMAIL_OWNER);
        }

        [TestMethod]
        public void SerializeEMailAddressTest() 
        {
            var testAddress = new EMailAddress(Consts.SOURCE_EMAIL, Consts.SOURCE_EMAIL_OWNER);
            var expected = $"{{\"email\":\"{Consts.SOURCE_EMAIL}\",\"name\":\"{Consts.SOURCE_EMAIL_OWNER}\"}}";
            var actual = JsonConvert.SerializeObject(testAddress);
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeserializeEMailAddressTest()
        {
            var expected = new EMailAddress(Consts.SOURCE_EMAIL, Consts.SOURCE_EMAIL_OWNER);
            var source = $"{{\"email\":\"{Consts.SOURCE_EMAIL}\",\"name\":\"{Consts.SOURCE_EMAIL_OWNER}\"}}";
            var actual = JsonConvert.DeserializeObject<EMailAddress>(source);

            Assert.AreEqual(expected.EMail, actual.EMail);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
