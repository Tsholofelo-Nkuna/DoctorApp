using DoctorManagement.Api.Consumer.Interfaces.Base;
using DoctorManagement.MAUI.Components.Pages.Interfaces.Base;
using DoctorManagement.Presentation.Components.Base;
using DoctorManagement.Presentation.Interface.Base;
using DoctorManagement.Presentation.ViewModels.Base;
using DoctorManagement.Shared.Constants;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Shared.Models;
using DoctorManagement.Shared.Models.Base;



namespace DoctorManagement.MAUI.Components.Pages.Base
{
    public class BasePage<TViewModel, TDto> : BaseComponent<TViewModel, TDto>, IBasePage<TViewModel, TDto> where TViewModel: IBaseViewModel<TDto>, new() where TDto: new()
    {
        public async Task InitializeApiConsumers(List<IUnauthorizedApiCallHandler> apiConsumers)
        {
            var token = await SecureStorage.Default.GetAsync(LocalStorageKeys.BearerToken);
            foreach (var apiConsumer in apiConsumers)
            {
              if(apiConsumer is not null)
                {
                    apiConsumer.Unauthorized += this.OnUnauthorized;
                    if (token is string tknString)
                    {
                        apiConsumer.AccessToken = tknString;
                    }
                }
            }
        }

        public void ReleaseApiConsumers(List<IUnauthorizedApiCallHandler> apiConsumers)
        {
            foreach (var apiConsumer in apiConsumers)
            {
               if(apiConsumer is not null)
                {
                    apiConsumer.Unauthorized -= this.OnUnauthorized;
                }
            }
        }
    }
}
