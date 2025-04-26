using DoctorManagement.Presentation.Interface.Base;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Shared
{
    public partial class ProfileItemComponent
    {
        [Parameter]
        public RenderFragment<ProfileItemViewModel>? ContextMenu { get; set; }
        public ModalViewModel<DtoBase> ContextMenuModalViewModel { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public Task OnItemClick()
        {
            if (ViewModel.EnableContextMenu) { 
               ContextMenuModalViewModel.Show = !ContextMenuModalViewModel.Show;
            }
            return Task.CompletedTask;
        }
    }

  
}
