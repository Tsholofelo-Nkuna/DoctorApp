using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.Presentation.Interface.Base;
using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.MAUI.Components.Pages.Interfaces.Base
{
    public interface IBasePage<TViewModel, TDto> : IBaseComponent<TViewModel, TDto> where TViewModel: IBaseViewModel<TDto>, new() where TDto: new()
    {
        public Task InitializeApiConsumers(List<IUnauthorizedApiCallHandler> apiConsumers);
        public void ReleaseApiConsumers(List<IUnauthorizedApiCallHandler> apiConsumers);
        
    }

    
}
