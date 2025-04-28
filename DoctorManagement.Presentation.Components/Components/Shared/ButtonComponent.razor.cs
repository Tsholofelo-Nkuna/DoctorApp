using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    public partial class ButtonComponent
    {
        [Parameter]
        public string Type { get; set; } = "primary";
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; } = new();
        [Parameter]
        public RenderFragment? Content { get; set; }
        [Parameter]
        public bool Loading { get; set; }
        [Parameter]
        public EventCallback<ButtonComponent> OnButtonClick { get; set; }

        public async Task OnButtonClicked()
        {
            if (!Loading)
            {
                await OnButtonClick.InvokeAsync(this);
            }
          
        }
    }
}
