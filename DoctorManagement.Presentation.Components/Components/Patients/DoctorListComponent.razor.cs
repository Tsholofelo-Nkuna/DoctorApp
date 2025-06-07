using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Presentation.Components.Base;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Patients
{
    public partial class DoctorListComponent:  BaseComponent<DoctorListViewModel, List<DoctorDto>>
    {

        [Parameter, EditorRequired]
        public Func<(double x1, double y1), (double x2, double y2), double>? DistanceCalculator { get; set; }

        [Parameter, EditorRequired]
        public (double Latitude, double Longitude) CurrentAppLocation { get; set; } = (0, 0);
       
        [Parameter]
        public EventCallback<PageRequestDto<DoctorFilter>> OnDoctorSearchClick { get; set; }
        [Inject]
        public IDataSourceApiConsumer DataSourceApiConsumer { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await this.GetData();
        }

        public  async Task OnDoctorSpecialtyDropListChange(object? val)
        {
            if(val is object validValue)
            {
                var value = Convert.ToInt32(validValue);
                ViewModel.DocSearchModalViewModel.Data.SpecialtyValue = value;
                await OnFindDoctorSecondButtonClicked();
            }
         
          
        }
        public Task OnFindDoctorClicked()
        {
            ViewModel.DocSearchModalViewModel.Show = true;
            return Task.CompletedTask;
        }

        public async Task GetData()
        {
            this.ViewModel.DoctorSpecialtyList  =  (await this.DataSourceApiConsumer
                .Get(new() { GetAllPages = true, Filter = new() { TypeCode = DataSourceTypeCodeConstants.Specialty } }))?.Data ?? [];
                
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

        public Task OnScheduleButtonClicked(DoctorDto doctor)
        {
            this.NavManager.NavigateTo($"{RouteConstants.ScheduleAppointment}/{doctor.Id}");
            return Task.CompletedTask;
        }
    }
}
