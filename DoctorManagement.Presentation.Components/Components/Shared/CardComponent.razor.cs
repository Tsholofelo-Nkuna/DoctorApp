using DoctorManagement.Presentation.Components.Base;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Presentation.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    
    public partial class CardComponent<TDto>
    {
        [Parameter]
        public RenderFragment<CardComponentViewModel<TDto>>? Header  {get; set;}
        [Parameter]
        public RenderFragment<CardComponentViewModel<TDto>>? Body { get; set; }
        [Parameter]
        public RenderFragment<CardComponentViewModel<TDto>>? Footer { get; set; }
    }
}
