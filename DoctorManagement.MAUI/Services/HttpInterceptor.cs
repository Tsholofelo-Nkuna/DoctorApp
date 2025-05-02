using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Services
{
    public class HttpInterceptor : DelegatingHandler
    {
        private readonly NavigationManager _navManagaer;
        public HttpInterceptor(NavigationManager navigationManager) {
          _navManagaer = navigationManager;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if(response is { StatusCode : HttpStatusCode.Unauthorized })
            {
                this._navManagaer.NavigateTo(RouteConstants.Login);
            }
            return response;
        }
    }
}
