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

        public bool FormEditContextIsValid
        {
            get
            {
                return  this.CredentialsEditContext.Validate()
                    && this.AddressEditContext.Validate()
                    && this.ContactEditContext.Validate();
            }
        }
        protected override Task OnInitializedAsync()
        {
            var returned =  base.OnInitializedAsync();
            this.ViewModel.Data.DoctorDetails.PracticeSite = new();
            this.ViewModel.Data.DoctorDetails.Contact = new();
            this.ViewModel.Title = "Sign up as a doctor";
            this.AddressEditContext = new(ViewModel.Data.DoctorDetails.PracticeSite);
            this.ContactEditContext = new(ViewModel.Data.DoctorDetails.Contact);
            this.CredentialsEditContext = new(ViewModel.Data.Credentials);
            return returned;
        }

        public Task OnSubmitCliked()
        {
            if (this.FormEditContextIsValid)
            {

            }
            return Task.CompletedTask;
        }
    }
}
