
using DoctorManagement.Api.Consumer;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.MAUI.Services;
using DoctorManagement.MAUI.Services.Interfaces;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages
{
    public partial class Doctors
    {
        [Inject]
        public IAppLocationService LocationService { get; set; }
        public Location? AppLocation { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IDoctorApiConsumer DoctorApiConsumer { get; set; }

        private  PageResponse<DoctorDto> _doctorListPageResponse;
        public PageRequestDto<DoctorFilter> DoctorListPageRequestDto {
            get;
            set;
        } = new();

        public PageResponse<DoctorDto> DoctorListPageResponse
        {
            get { return _doctorListPageResponse; }
            set
            {
                _doctorListPageResponse = value;
                ViewModel.Data = _doctorListPageResponse?.Data?.ToList() ?? [];
            }
        }
        private string _doctorsController = "Doctors";
        //public Doctors(): base()
        //{
           
        //}
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            DoctorApiConsumer.Unauthorized += this.OnUnauthorized;
            var location = await LocationService.GetAppCurrentLocation();
            this.AppLocation = location;
            var token = await SecureStorage.Default.GetAsync(LocalStorageKeys.BearerToken);
            if(token is not null)
            {
               // var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(token);
                DoctorApiConsumer.AccessToken = token;
            }
            this.DoctorListPageResponse = await this.GetDoctorsList(this.DoctorListPageRequestDto);
            
        }

        public async Task OnFindDoctorSecondButtonClicked(PageRequestDto<DoctorFilter> pageRequest)
        {

            var results = await this.GetDoctorsList(pageRequest);
            if (results is not null)
            {
                ViewModel.Data = results.Data?.ToList() ?? [];
            }
            ViewModel
                .DocSearchModalViewModel.Show = false;
        }

        public double ComputeDistance((double Longitude, double Latitude) point1, (double Longitude, double Latitude ) point2)
        {
            return Location.CalculateDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, DistanceUnits.Kilometers);
        }

        public async Task<PageResponse<DoctorDto>> GetDoctorsList(PageRequestDto<DoctorFilter> pageRequest)
        {
            var results = await this.DoctorApiConsumer.Get(new()
            {
                Filter = pageRequest.Filter,
                GetAllPages = false,
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize,
            }, this._doctorsController) ?? new();

            return results;
        }
        ~Doctors()
        {
            if(DoctorApiConsumer is not null)
            {
                DoctorApiConsumer.Unauthorized -= this.OnUnauthorized;
            }
           
        }
    }
}
