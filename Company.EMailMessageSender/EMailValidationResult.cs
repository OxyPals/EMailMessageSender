using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.EMailMessageSender
{
    public class EMailValidationResult
    {
        /// <summary>
        /// If True validation completed successfully
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// If validation completed with errors contains errors.
        /// </summary>
        public List<string> Errors { get; private set; }
        /// <summary>
        /// If validation completed with errors contains errors as exceptions.
        /// </summary>
        public AggregateException ErrorsException { get; set; }
        /// <summary>
        /// Initializes new instance of validation result
        /// </summary>
        public EMailValidationResult() 
        {
            Errors = new List<string>();
            ErrorsException = new AggregateException();
        }
        /// <summary>
        /// Helper method for creating successfull result.
        /// </summary>
        /// <returns>Successful result</returns>
        public static EMailValidationResult Ok() 
        {
            return new EMailValidationResult { Success = true };
        }
        /// <summary>
        /// Helper method to create failed result.
        /// </summary>
        /// <param name="errors">List of errors</param>
        /// <returns>Failed result</returns>
        /// <exception cref="ArgumentNullException">If errors argument is null.</exception>
        public static EMailValidationResult Fail(IEnumerable<string> errors) 
        {
            if (null == errors) throw new ArgumentNullException(nameof(errors));

            return new EMailValidationResult { Success=false , Errors = new List<string>(errors)};
        }

        /// <summary>
        /// Helper method to create failed result.
        /// </summary>
        /// <param name="exception">AgregateExceptions with errors as inner exceptions</param>
        /// <returns>Failed result</returns>
        /// <exception cref="ArgumentNullException">If exception argument is null.</exception>
        public static EMailValidationResult Fail(AggregateException exception) 
        {
            if (null == exception) throw new ArgumentNullException(nameof (exception));
            var exceptionMessages = new List<string>(exception.InnerExceptions.Select(x => x.Message));

            return new EMailValidationResult { Success = false, Errors = exceptionMessages, ErrorsException = exception };
        }
    }
}
