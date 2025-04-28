using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using DoctorManagement.Presentation.Components.Shared;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Shared.DataTransferObjects;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignupComponent
    {
        [Inject]
        private IIdentityApiConsumer? _identityApiConsumer { get; set; }
        public EditContext ContactEditContext { get; set; }
        public EditContext CredentialsEditContext { get; set; }
        public EditContext AddressEditContext { get; set; }
        public ModalViewModel<SignupDto> ModalViewModel { get; set; } = new();
        public ResponseDto<bool> ServerMessage { get; set; } = new();
        [Parameter]
        public EventCallback CurrentLocationButtonClick { get; set; }
        private bool _submitLoading = false;
        [Parameter]
        public bool CurrentLocationButtonLoading { get; set; }
      
        public SignupComponent(): base(){
            this.ContactEditContext = new(ViewModel.Data.Contact);
            this.AddressEditContext = new(ViewModel.Data.Address);
            this.CredentialsEditContext = new(ViewModel.Data.Credentials);
            this.ModalViewModel.Data = ViewModel.Data;
        }

        public bool EditContextIsValid 
        {
            get
            {
                return  this.CredentialsEditContext.Validate()
                && this.AddressEditContext.Validate()
                && this.ContactEditContext.Validate();
            }
        }

        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return this.CredentialsEditContext.GetValidationMessages().Concat(this.AddressEditContext.GetValidationMessages())
                    .Concat(this.ContactEditContext.GetValidationMessages());
              
             
            }
        }
       
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public async Task OnSubmitClick(ButtonComponent sender)
        {
            _submitLoading = true;
            if (this.EditContextIsValid && this._identityApiConsumer is not null) {

                var result = await this._identityApiConsumer.PatientSignup(new()
                {
                    Address = ViewModel.Data.Address,
                    Contact = ViewModel.Data.Contact,
                    Credentials = ViewModel.Data.Credentials,
                });
                if(result is not null)
                {
                    this.ServerMessage = result;
                }
                else
                {
                    this.ServerMessage.Message = "Unkown error occured!";
                    this.ServerMessage.Data = false;
                }

                ModalViewModel.Show = true;
               
            }
            _submitLoading = false;

        }

        public Task OnCloseModal()
        {
            ModalViewModel.Show = false;
            return Task.CompletedTask;
        }
    }
}
