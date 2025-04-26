using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Presentation.Components.Base;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Presentation.ViewModels.Base;
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
        [Inject]
        public IDoctorApiConsumer DoctorApiConsumer { get; set; }

        private PageResponse<DoctorDto> _pageResponse = new();
        public PageResponse<DoctorDto> PageResponse { 
            get => _pageResponse;
            set
            {
                _pageResponse = value;
                if(_pageResponse.Data is IEnumerable<DoctorDto> data && data.Any())
                {
                    this.ViewModel.Data = data.ToList();
                }
                
            }
        }
        public ModalViewModel<DoctorFilter> DocSearchModalViewModel { get; set; } = new();
        public PageRequestDto<DoctorDto> PageRequestDto { get; set; } = new();
        private string _doctorsController = "Doctors";
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.PageResponse = await this.GetNextPageData(new() { PageIndex = 0});
    
        }

        public Task OnFindDoctorClicked()
        {
            DocSearchModalViewModel.Show = true;
            return Task.CompletedTask;
        }

        public async Task OnFindDoctorSecondButtonClicked()
        {
            var results = await this.DoctorApiConsumer.Get(new()
            {
                PageIndex = 1,
                Filter = DocSearchModalViewModel.Data
            }, "Doctors");
            if(results is not null)
            {
                PageResponse = results;
            }
            DocSearchModalViewModel.Show = false;
        }
        public async Task<PageResponse<DoctorDto>> GetNextPageData(PageRequestDto<DoctorFilter> pageRequest)
        {
            var results = await this.DoctorApiConsumer.Get(new()
            {
                Filter = pageRequest.Filter,
                GetAllPages = false,
                PageIndex = pageRequest.PageIndex + 1,
                PageSize = pageRequest.PageSize,
            }, this._doctorsController) ?? new();

            return results;
        }
    }
}
