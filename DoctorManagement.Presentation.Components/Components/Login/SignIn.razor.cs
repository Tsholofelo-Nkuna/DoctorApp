
using DoctorManagement.Api.Consumer.Interfaces;
using Microsoft.AspNetCore.Components;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignIn 
    {
        [Inject]
        public IIdentityApiConsumer IdentityApiConsumer { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
          
        }

        public async Task Login()
        {
            this.NavManager.NavigateTo("/Doctors");

            //if (this.ViewModel.EditContext.Validate())
            //{
            //    try
            //    {
            //        var response = await this.IdentityApiConsumer.Login(this.ViewModel.Data.Username, this.ViewModel.Data.Password);
            //        if (response is { Data: true })
            //        {
            //            this.NavManager.NavigateTo("/Doctors");
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}
           
        }
    }
}
