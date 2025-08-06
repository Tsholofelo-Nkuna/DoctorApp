using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Interfaces.Base
{
    public interface IApiConsumerBase<TDto, TFilter>: IUnauthorizedApiCallHandler where TDto : new() where TFilter : BaseFilter, new()
    {
        public string AccessToken { get; set; }
        public Task<PageResponse<TDto>?> Get(PageRequestDto<TFilter> pageRequest);
        public Task<ResponseDto<bool>?> Delete(Guid guid);
        public Task<ResponseDto<IEnumerable<Guid>>?> Add(TDto dto);
        public Task<ResponseDto<IEnumerable<string>>?> GetUserRoles();
        public Task<ResponseDto<string>> GetUserId();

    }
}
