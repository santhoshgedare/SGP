using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Interfaces.IServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(List<string> email, string subject, string message);
        Task SendEmailAsync(List<string> emails, string subject, string message, List<string> ccemails);
    }
}
