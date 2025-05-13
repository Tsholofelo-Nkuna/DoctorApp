using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Services.Interfaces
{
    public interface IMailTemplateMessageHandler
    {
        public Task<string> MailTemplateMessage(Type componentType, Dictionary<string, object?>? componentParameters);
        public Task SendTemplateMessageAsync(string toEmail, string subject, Type componentType, Dictionary<string, object?>? componentParameters);
    }
}
