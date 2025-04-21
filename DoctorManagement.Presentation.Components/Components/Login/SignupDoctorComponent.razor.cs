using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignupDoctorComponent
    {
        protected override Task OnInitializedAsync()
        {
            var returned =  base.OnInitializedAsync();
            this.ViewModel.Data.DoctorDetails.PracticeSite = new();
            this.ViewModel.Data.DoctorDetails.Contact = new();
            this.ViewModel.Title = "Sign up as a doctor";
            return returned;
        }
    }
}
