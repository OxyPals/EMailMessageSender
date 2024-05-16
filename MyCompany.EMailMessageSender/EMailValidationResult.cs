using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.EMailMessageSender
{
    public class EMailValidationResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; private set; }

        public AggregateException ErrorsException { get; set; }

        public EMailValidationResult() 
        {
            Errors = new List<string>();
            ErrorsException = new AggregateException();
        }

        public static EMailValidationResult Ok() 
        {
            return new EMailValidationResult { Success = true };
        }

        public static EMailValidationResult Fail(IEnumerable<string> errors) 
        {
            if (null == errors) throw new ArgumentNullException(nameof(errors));

            return new EMailValidationResult { Success=false , Errors = new List<string>(errors)};
        }

        public static EMailValidationResult Fail(AggregateException exception) 
        {
            if (null == exception) throw new ArgumentNullException(nameof (exception));
            var exceptionMessages = new List<string>(exception.InnerExceptions.Select(x => x.Message));

            return new EMailValidationResult { Success = false, Errors = exceptionMessages, ErrorsException = exception };
        }
    }
}
