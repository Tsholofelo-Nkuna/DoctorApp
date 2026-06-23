using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignupDoctorComponent
    {
        public EditContext AddressEditContext { get; set; }
        public EditContext ContactEditContext { get; set; }
        public EditContext CredentialsEditContext { get; set; }
        public EditContext DoctorEditContext { get; set; }
        [Inject]
        public IIdentityApiConsumer IdentityApiConsumer { get; set; }
        [Inject]
        public IDataSourceApiConsumer DataSourceApiConsumer { get; set; }
        public ResponseDto<IEnumerable<Guid>>? ServiceResponse { get; set; } = new();
        public IEnumerable<DataSourceDto> TitleOptions { get; set; } = [];
        public IEnumerable<DataSourceDto> DoctorSpecialties { get; set; } = [];
        public ModalViewModel<SignupDoctorDto> ModalViewModel { get; set; } = new();
        [Parameter]
        public EventCallback UseCurrentLocationClick { get; set; }
        [Parameter]
        public bool UseCurrentLocationButtonLoading { get; set; }
        private bool _submitButtonLoading = false;
        [Parameter]
        public EventCallback TakePhotoClick { get; set; }
        public bool FormEditContextIsValid
        {
            get
            {
                return  this.CredentialsEditContext.Validate()
                    && this.AddressEditContext.Validate()
                    && this.ContactEditContext.Validate()
                    && this.DoctorEditContext.Validate();
            }
        }

        public async Task OnTakePhotoClick()
        {

            await TakePhotoClick.InvokeAsync();
        }
        public IEnumerable<string> ValidationMessages
        {
            get
            {
                return this.DoctorEditContext.GetValidationMessages()
                    .Concat(this.CredentialsEditContext.GetValidationMessages())
                    .Concat(this.ContactEditContext.GetValidationMessages())
                    .Concat(this.AddressEditContext.GetValidationMessages());
            }
        }
        protected override async Task OnInitializedAsync()
        {
            var returned =  base.OnInitializedAsync();
            this.ViewModel.Data.PracticeSite = new();
            this.ViewModel.Data.Contact = new();
            this.ViewModel.Title = "Sign up as a doctor";
            this.AddressEditContext = new(ViewModel.Data.PracticeSite);
            this.ContactEditContext = new(ViewModel.Data.Contact);
            this.CredentialsEditContext = new(ViewModel.Data.Credentials);
            this.DoctorEditContext = new(ViewModel.Data);
            this.ModalViewModel.Data = ViewModel.Data;
          
            await this.GetData();
         
        }

        public async Task GetData()
        {
            var response = await this.DataSourceApiConsumer.Get(new() { GetAllPages = true, Filter = new() { TypeCode = DataSourceTypeCodeConstants.ProfessionalTitle } });
            var doctorSpecialtyListResponse = await this.DataSourceApiConsumer.Get(new() { GetAllPages = true, Filter = new() { TypeCode = DataSourceTypeCodeConstants.Specialty } });
            if (response is {Data: IEnumerable<DataSourceDto> } responseContent)
            {
                this.TitleOptions = responseContent.Data;
                //StateHasChanged();
            }
            if (doctorSpecialtyListResponse is { Data: IEnumerable<DataSourceDto> } specialtyListResponse)
            {
                this.DoctorSpecialties = specialtyListResponse.Data;
                //StateHasChanged();
            }
        }

        
        public async Task OnSubmitCliked()
        {
            _submitButtonLoading = true;
            if (this.FormEditContextIsValid)
            {
                var serviceResponse = await this.IdentityApiConsumer.DoctorSignup(ViewModel.Data);

                if(serviceResponse is not null)
                {
                    this.ServiceResponse = serviceResponse;
                }
                else
                {
                    this.ServiceResponse ??= new();
                    this.ServiceResponse.Data = [];
                    this.ServiceResponse.Message = "Unkown error occured";
                }

                ModalViewModel.Show = true;
              
            }
            _submitButtonLoading = false;
        }
        public Task OnTitleFieldChange(ChangeEventArgs args)
        {  
           var value = args.Value?.ToString();
           _ = int.TryParse(value, out var val);
           this.ViewModel.Data.TitleDescription = this.TitleOptions.FirstOrDefault(x => x.Value == val)?.Description ?? string.Empty;
            
            return Task.CompletedTask;
        }
        public Task OnCloseModal()
        {
            ModalViewModel.Show = false;
            return Task.CompletedTask;
        }
    }
}
