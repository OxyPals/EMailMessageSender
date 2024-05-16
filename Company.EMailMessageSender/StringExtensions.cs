using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.EMailMessageSender
{
    /// <summary>
    /// Contains some extension methods for string class
    /// </summary>
    public static class StringExtensions
    {
        private const string EMAIL_PATTERN = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        private static readonly Regex EMailChecker = new Regex(EMAIL_PATTERN);

        /// <summary>
        /// Checks if string is a valid EMail address
        /// </summary>
        /// <remarks>
        /// We use regular expression of format "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
        /// This expression does not support new domains like .info. For production use we need to replace this check with more actual one.
        /// </remarks>
        /// <param name="source">String to check</param>
        /// <returns>True if string contains valid email address, False otherwise.</returns>
        public static bool IsEMail(this string source) 
        {
             return EMailChecker.IsMatch(source);
        }
    }
}
