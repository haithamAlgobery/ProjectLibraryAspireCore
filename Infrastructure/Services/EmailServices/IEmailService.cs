using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EmailServices
{
    public interface IEmailService
    {
        void SendWelcomeEmail(string toEmail, string bookTitle);
    }
}
