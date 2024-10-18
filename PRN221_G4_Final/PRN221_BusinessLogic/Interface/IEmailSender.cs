using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string body);
    }
}
