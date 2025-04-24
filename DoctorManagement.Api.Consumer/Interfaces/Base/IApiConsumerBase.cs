using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer.Interfaces.Base
{
    public interface IApiConsumerBase<TDto, TFilter> where TDto : new() where TFilter : BaseFilter, new()
    {
        public string AccessToken { get; set; }
        public Task<PageResponse<TDto>?> Get(PageRequestDto<TFilter> pageRequest, string controller);
        public Task<ResponseDto<bool>?> Delete(Guid guid, string controller);
        public Task<ResponseDto<bool>?> Add(TDto dto, string controller);
        public (string accessToken, string refreshToken) GetAccessToken(string username, string password);
    }
}
