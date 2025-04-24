using DoctorManagement.Presentation.Interface.Base;

using Microsoft.AspNetCore.Components;


namespace DoctorManagement.Presentation.Components.Base
{ 
   
    public class BaseComponent<TViewModel, TDto> : ComponentBase, IBaseComponent<TViewModel, TDto> where TDto: new() where TViewModel : IBaseViewModel<TDto>, new()
    {
     

        [Parameter]
        public TViewModel ViewModel { get; set; } = new();
        [Inject]
        public NavigationManager NavManager { get; set; }
    }
}
