using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Api.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using DoctorManagement.Presentation.Components.Shared;
using DoctorManagement.Presentation.ViewModels;
using DoctorManagement.Shared.DataTransferObjects;
using Org.BouncyCastle.Asn1.X509;

namespace DoctorManagement.Presentation.Components.Login
{
    public partial class SignupComponent : IDisposable
    {
        [Inject]
        private IIdentityApiConsumer? _identityApiConsumer { get; set; }
        public EditContext ContactEditContext { get; set; }
        public EditContext CredentialsEditContext { get; set; }
        public EditContext AddressEditContext { get; set; }
        public EditContext GeneralInfoEditContext { get; set; }
        public ModalViewModel<SignupDto> ModalViewModel { get; set; } = new();
        public ResponseDto<IEnumerable<Guid>>? ServerMessage { get; set; } = new();
        [Parameter]
        public EventCallback CurrentLocationButtonClick { get; set; }
        [Parameter]
        public EventCallback TakePhotoClick { get; set; }
        private bool _submitLoading = false;
        [Parameter]
        public bool CurrentLocationButtonLoading { get; set; }
        public bool ShowMedicalAidPlanNameField { get; set; }
        public SignupComponent(): base(){
            this.ContactEditContext = new(ViewModel.Data.Contact);
            this.AddressEditContext = new(ViewModel.Data.Address);
            this.CredentialsEditContext = new(ViewModel.Data.Credentials);
            this.GeneralInfoEditContext = new(ViewModel.Data.GeneralInfo);
            this.ModalViewModel.Data = ViewModel.Data;
        }

        protected override Task OnInitializedAsync()
        {
            GeneralInfoEditContext.OnFieldChanged += OnGeneralInfoFieldChange;
            return base.OnInitializedAsync();
        }

        public void OnGeneralInfoFieldChange(object? sender, FieldChangedEventArgs eventArgs)
        {
            if(eventArgs.FieldIdentifier.FieldName == nameof(GeneralInfo.MedicalAidProvider) && eventArgs.FieldIdentifier.Model is GeneralInfo gInfoModel)
            {
                if (!string.IsNullOrWhiteSpace(gInfoModel.MedicalAidProvider))
                {
                    ShowMedicalAidPlanNameField = true;
                }
                else
                {
                    ShowMedicalAidPlanNameField = false;
                }
            }
        }
        public bool EditContextIsValid 
        {
            get
            {
                return  this.CredentialsEditContext.Validate()
                && this.GeneralInfoEditContext.Validate()
                && this.AddressEditContext.Validate()
                && this.ContactEditContext.Validate();
            }
        }

        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return this.CredentialsEditContext.GetValidationMessages()
                    .Concat(this.AddressEditContext.GetValidationMessages())
                    .Concat(this.ContactEditContext.GetValidationMessages())
                    .Concat(this.GeneralInfoEditContext.GetValidationMessages());
             
            }
        }
       
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task OnTakePhotoClick() {
            
            await TakePhotoClick.InvokeAsync();
        }
        public async Task OnSubmitClick(ButtonComponent sender)
        {
            _submitLoading = true;
            if (this.EditContextIsValid && this._identityApiConsumer is not null && ViewModel.Data.PhotoContents.Any()) {

                var result = await this._identityApiConsumer.PatientSignup(new()
                {
                    Address = ViewModel.Data.Address,
                    Contact = ViewModel.Data.Contact,
                    Credentials = ViewModel.Data.Credentials,
                    GeneralInfo = ViewModel.Data.GeneralInfo,
                    PhotoContents = ViewModel.Data.PhotoContents,
                    PhotoFileName = ViewModel.Data.PhotoFileName,
                    PhotoMimeType = ViewModel.Data.PhotoMimeType,
                });
                if(result is not null)
                {
                    this.ServerMessage = result;
                }
                else
                {
                    this.ServerMessage = new();
                    this.ServerMessage.Message = "Unkown error occured!";
                    this.ServerMessage.Data = [];
                }

                ModalViewModel.Show = true;
               
            }
            _submitLoading = false;

        }

        public Task OnCloseModal()
        {
            ModalViewModel.Show = false;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GeneralInfoEditContext.OnFieldChanged -= OnGeneralInfoFieldChange;
        }
    }
}
