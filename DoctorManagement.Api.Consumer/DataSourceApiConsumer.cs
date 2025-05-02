using DoctorManagement.Api.Consumer.Base;
using DoctorManagement.Api.Consumer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Api.Consumer
{
    public class DataSourceApiConsumer : ApiConsumerBase<DataSourceDto, DataSourceFilter>, IDataSourceApiConsumer
    {
        public DataSourceApiConsumer(
            IOptions<ApiOptions> options,
            IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
        }
    }
}
