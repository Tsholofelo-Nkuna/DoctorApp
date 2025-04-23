using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignupDoctorComponent
    {
        public EditContext AddressEditContext { get; set; }
        public EditContext ContactEditContext { get; set; }
        public EditContext CredentialsEditContext { get; set; }
        public EditContext DoctorEditContext { get; set; }
        [Inject]
        public IIdentityApiConsumer IdentityApiConsumer { get; set; }
        public ResponseDto<bool> ServiceResponse { get; set; } = new();
        public ModalViewModel<SignupDoctorDto> ModalViewModel { get; set; } = new();
        public bool FormEditContextIsValid
        {
            get
            {
                return  this.CredentialsEditContext.Validate()
                    && this.AddressEditContext.Validate()
                    && this.ContactEditContext.Validate()
                    && this.DoctorEditContext.Validate();
            }
        }

        public IEnumerable<string> ValidationMessages
        {
            get
            {
                return this.DoctorEditContext.GetValidationMessages()
                    .Concat(this.CredentialsEditContext.GetValidationMessages())
                    .Concat(this.ContactEditContext.GetValidationMessages())
                    .Concat(this.AddressEditContext.GetValidationMessages());
            }
        }
        protected override Task OnInitializedAsync()
        {
            var returned =  base.OnInitializedAsync();
            this.ViewModel.Data.PracticeSite = new();
            this.ViewModel.Data.Contact = new();
            this.ViewModel.Title = "Sign up as a doctor";
            this.AddressEditContext = new(ViewModel.Data.PracticeSite);
            this.ContactEditContext = new(ViewModel.Data.Contact);
            this.CredentialsEditContext = new(ViewModel.Data.Credentials);
            this.DoctorEditContext = new(ViewModel.Data);
            this.ModalViewModel.Data = ViewModel.Data;
            return returned;
        }

        public async Task OnSubmitCliked()
        {
            if (this.FormEditContextIsValid)
            {
                var serviceResponse = await this.IdentityApiConsumer.DoctorSignup(ViewModel.Data);

                if(serviceResponse is not null)
                {
                    this.ServiceResponse = serviceResponse;
                }
                else
                {
                    this.ServiceResponse.Data = false;
                    this.ServiceResponse.Message = "Unkown error occured";
                }

                ModalViewModel.Show = true;
            }
        }
        public Task OnCloseModal()
        {
            ModalViewModel.Show = false;
            return Task.CompletedTask;
        }
    }
}
