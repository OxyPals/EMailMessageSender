using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.EMailMessageSender
{
    public interface IEMailMessageSender
    {
        EMailSendResult SendMessage(EMailMessage message);
    }
}
