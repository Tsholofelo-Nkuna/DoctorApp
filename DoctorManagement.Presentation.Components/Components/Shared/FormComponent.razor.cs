using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    public partial class FormComponent<TDto>
    {
        public string InputFielColClass => $"col-{Math.Ceiling(12 / (Convert.ToDouble(ViewModel.InputsPerRowCount, null)))}";
        [Parameter]
        public EventCallback OnValidSubmission { get; set; }
        public async Task OnValidSubmit(EditContext editContext)
        {
            if (ViewModel.EditContext.Validate())
            {
                await OnValidSubmission.InvokeAsync();
            }
        }
    }
}
