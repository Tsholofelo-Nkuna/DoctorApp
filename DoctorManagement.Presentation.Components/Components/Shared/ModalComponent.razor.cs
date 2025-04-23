using DoctorManagement.Presentation.Components.Base;
using DoctorManagement.Presentation.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    public partial class ModalComponent<TDto>: BaseComponent<ModalViewModel<TDto>, TDto>
    {
       [Parameter]
       public RenderFragment<ModalViewModel<TDto>>? Header { get; set; }
       [Parameter]
       public RenderFragment<ModalViewModel<TDto>>? Body { get; set; }
       [Parameter]
       public RenderFragment<ModalViewModel<TDto>>? Footer { get; set; }
    }
}
