using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    public partial class ProfileItemComponent
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var x = this.ViewModel.Data;
        }
    }
}
