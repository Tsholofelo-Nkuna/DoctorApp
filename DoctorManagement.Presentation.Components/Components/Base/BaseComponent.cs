using Blazored.LocalStorage;
using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Presentation.Interface.Base;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;

using Microsoft.AspNetCore.Components;


namespace DoctorManagement.Presentation.Components.Base
{ 
   
    public class BaseComponent<TViewModel, TDto> : ComponentBase, IBaseComponent<TViewModel, TDto> where TDto: new() where TViewModel : IBaseViewModel<TDto>, new()
    {
     

        [Parameter]
        public TViewModel ViewModel { get; set; } = new();
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected virtual void OnUnauthorized(object? sender, ResponseDto<bool> eventArgs)
        {
            this.NavManager.NavigateTo(RouteConstants.Login);
        }
    }
}
