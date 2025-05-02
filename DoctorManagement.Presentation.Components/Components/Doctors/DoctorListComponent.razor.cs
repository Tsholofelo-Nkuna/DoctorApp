using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Presentation.Components.Base;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Doctors
{
    public partial class DoctorListComponent:  BaseComponent<DoctorListViewModel, List<DoctorDto>>
    {

        [Parameter, EditorRequired]
        public Func<(double x1, double y1), (double x2, double y2), double>? DistanceCalculator { get; set; }

        [Parameter, EditorRequired]
        public (double Latitude, double Longitude) CurrentAppLocation { get; set; } = (0, 0);
       
        [Parameter]
        public EventCallback<PageRequestDto<DoctorFilter>> OnDoctorSearchClick { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public Task OnFindDoctorClicked()
        {
            ViewModel.DocSearchModalViewModel.Show = true;
            return Task.CompletedTask;
        }

        public async Task OnFindDoctorSecondButtonClicked()
        {
               await OnDoctorSearchClick.InvokeAsync(new()
               {
                   Filter = ViewModel.DocSearchModalViewModel.Data,
                   PageIndex = 1,
                   GetAllPages = true
               });
        }
    }
}
