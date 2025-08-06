using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.MAUI.Services.Interfaces;

using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.AspNetCore.Components;


namespace DoctorManagement.MAUI.Components.Pages.Patient
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

        private PageResponse<DoctorDto> _doctorListPageResponse;
        public PageRequestDto<DoctorFilter> DoctorListPageRequestDto
        {
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

        public List<IUnauthorizedApiCallHandler> ApiConsumers = [];
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.ApiConsumers = [
                DoctorApiConsumer,
                ];
            await this.InitializeApiConsumers(this.ApiConsumers);
         
            var location = await LocationService.GetAppCurrentLocation();
            AppLocation = location;
            DoctorListPageResponse = await GetDoctorsList(DoctorListPageRequestDto);

        }

        public async Task OnFindDoctorSecondButtonClicked(PageRequestDto<DoctorFilter> pageRequest)
        {

            var results = await GetDoctorsList(pageRequest);
            if (results is not null)
            {
                ViewModel.Data = results.Data?.ToList() ?? [];
            }
            ViewModel
                .DocSearchModalViewModel.Show = false;
        }

        public double ComputeDistance((double Longitude, double Latitude) point1, (double Longitude, double Latitude) point2)
        {
            return Location.CalculateDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, DistanceUnits.Kilometers);
        }

        public async Task<PageResponse<DoctorDto>> GetDoctorsList(PageRequestDto<DoctorFilter> pageRequest)
        {
            var results = await DoctorApiConsumer.Get(new()
            {
                Filter = pageRequest.Filter,
                GetAllPages = false,
                PageIndex = pageRequest.PageIndex,
                PageSize = pageRequest.PageSize,
            }) ?? new();

            return results;
        }
        ~Doctors()
        {
            this.ReleaseApiConsumers(this.ApiConsumers);

        }
    }
}
