using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN221_BusinessLogic.Interface;

namespace PRN221_BusinessLogic.Service
{
    public class ApiEmailSender : IEmailSender
    {
        private readonly PRN221_Models.Models.Sender _config;
        public ApiEmailSender(PRN221_Models.Models.Sender config)
        {
            _config = config;
        }
        public void SendEmail(string email, string subject, string body)
        {

        }
    }
}
