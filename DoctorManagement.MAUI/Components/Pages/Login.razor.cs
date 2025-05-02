using DoctorManagement.Shared;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages
{
  
    public partial class Login
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        public async Task OnTokenReceived(TokenResponseDto token)
        {
           
            await SecureStorage.Default.SetAsync(LocalStorageKeys.BearerToken, token.AccessToken);
            this.NavManager.NavigateTo(RouteConstants.Doctors);
            
        }
    }

}
