using DoctorManagement.Api.Consumer.Base;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.Extensions.Options;

namespace DoctorManagement.Api.Consumer
{
    public class PatientApiConsumer : ApiConsumerBase<PatientDto>, IPatientApiConsumer
    {
        public PatientApiConsumer(IOptions<ApiOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
        }
    }
}
