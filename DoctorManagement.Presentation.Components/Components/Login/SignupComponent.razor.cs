using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignupComponent
    {
        [Inject]
        private IIdentityApiConsumer _identityApiConsumer { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

        }
        public async Task OnSubmitClick()
        {
            try {
               
                if (this.ViewModel.EditContext?.Validate() ?? false)
                {
                    var result = await this._identityApiConsumer.PatientSignup(this.ViewModel.Data);
                    if (result is not null)
                    {

                    }
                    else
                    {

                    }
                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
