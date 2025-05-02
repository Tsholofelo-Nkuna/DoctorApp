
using DoctorManagement.Api.Consumer.Interfaces;

using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignIn 
    {
        [Inject]
        public IIdentityApiConsumer IdentityApiConsumer { get; set; }
       
        private bool _loginButtonLoading;

        [Parameter]
        public EventCallback<TokenResponseDto> OnTokenReceived { get; set; }
     
        protected override void OnInitialized()
        {
            base.OnInitialized();
          
        }

        public async Task Login()
        {
            
            _loginButtonLoading = true;
            if (this.ViewModel.EditContext.Validate())
            {
                try
                {
                    var response = await this.IdentityApiConsumer.Login(this.ViewModel.Data.Username, this.ViewModel.Data.Password);
                    if (response is { Data: TokenResponseDto })
                    {
                        await OnTokenReceived.InvokeAsync(response.Data);
                    }
                }
                catch (Exception ex)
                {

                    
                }
            }
            _loginButtonLoading = false;

        }
    }
}
