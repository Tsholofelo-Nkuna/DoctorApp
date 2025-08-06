using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.MAUI.Components.Layout;
using DoctorManagement.Shared;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages
{
  
    public partial class Login
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IDoctorApiConsumer DoctorApiConsumer { get; set; }

        public List<IUnauthorizedApiCallHandler> ApiConsumers { get; set; } = [];
        public async Task OnTokenReceived(TokenResponseDto token)
        {
           
            await SecureStorage.Default.SetAsync(LocalStorageKeys.BearerToken, token.AccessToken);
            ApiConsumers = [DoctorApiConsumer];
            await this.InitializeApiConsumers(ApiConsumers);
            var currentUserRoles =  (await DoctorApiConsumer.GetUserRoles())?.Data;
            if(currentUserRoles is IEnumerable<string> validUserRoles)
            {
                if (validUserRoles.Contains(RoleConstants.Patient))
                {
                    MainLayout.RenderPatientLayout = true;
                    this.NavManager.NavigateTo(RouteConstants.Doctors);
                }
                else if (validUserRoles.Contains(RoleConstants.Doctor))
                {
                    MainLayout.RenderPatientLayout = false;
                    this.NavManager.NavigateTo(RouteConstants.DoctorAppointments);
                }
                
            }
           
            
        }

        ~Login() {
            this.ReleaseApiConsumers(ApiConsumers);
        }
    }

}
