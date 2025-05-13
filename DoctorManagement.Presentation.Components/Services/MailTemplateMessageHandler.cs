using DoctorManagement.Presentation.Services.Interfaces;
using DoctorManagement.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Services
{
    public class MailTemplateMessageHandler : IMailTemplateMessageHandler
    {
       
        private readonly HtmlRenderer _htmlRenderer;
        private readonly MailSenderService _mailSenderService;
      
        public MailTemplateMessageHandler(IServiceProvider serviceProvider, MailSenderService mailSenderService) { 
            _htmlRenderer = new HtmlRenderer(serviceProvider, serviceProvider.GetRequiredService<ILoggerFactory>());
            _mailSenderService = mailSenderService;
        }
        public async Task<string> MailTemplateMessage(Type componentType, Dictionary<string, object?>? componentParameters)
        {
           var htmlMessage = await  _htmlRenderer.Dispatcher.InvokeAsync( async () =>
            {
                try
                {
                    HtmlRootComponent? componentResult = null;
                    if (componentParameters is Dictionary<string, object?> validParameters)
                    {
                        componentResult = await _htmlRenderer.RenderComponentAsync(componentType, ParameterView.FromDictionary(validParameters));
                    }
                    else
                    {
                        componentResult = await _htmlRenderer.RenderComponentAsync(componentType);
                    }

                    return componentResult?.ToHtmlString() ?? string.Empty;
                }
                catch (Exception ex)
                {

                    return string.Empty;
                }
            });
            return htmlMessage ?? string.Empty; 
        }

        public async Task SendTemplateMessageAsync(string toEmail, string subject, Type componentType, Dictionary<string, object?>? componentParameters)
        {
            var eMailMessage = await this.MailTemplateMessage(componentType, componentParameters);
            await _mailSenderService.SendMailAsync(toEmail, subject, eMailMessage ?? string.Empty);
        }
    }
}
