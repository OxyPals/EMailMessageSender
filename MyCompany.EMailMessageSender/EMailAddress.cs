using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyCompany.EMailMessageSender
{
    /// <summary>
    /// Represents email address
    /// </summary>
    public class EMailAddress
    {
        /// <summary>
        /// EMail address
        /// </summary>
        [JsonProperty("email")]
        [JsonRequired]
        public string EMail { get; private set; }
        /// <summary>
        /// EMail address owner name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Initializes new instance of email address
        /// </summary>
        /// <param name="email">EMail</param>
        /// <param name="name">Owner name</param>
        /// <exception cref="ArgumentNullException">If email or name are null, empty or contains only whitespaces</exception>
        /// <exception cref="ArgumentException">If email is not valid email address</exception>
        [JsonConstructor]
        public EMailAddress(string email, string name = "") 
        {
            if(string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");
            if(!email.IsEMail()) throw new ArgumentException($"{email} is not a valid email address.", nameof(email));
            
            // Name is optional right now. If you want to enforce checking of name value for Empty for all senders uncomment line below
            // Also you can implement custom message validation in specific sender ValidateMessage method
            //if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

            EMail = email;
            Name = name;
        }
    }
}
