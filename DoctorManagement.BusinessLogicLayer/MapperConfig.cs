using AutoMapper;
using DoctorManagement.DataAccessLayer.Entities;
using DoctorManagement.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            this.CreateMap<PatientDto, PatientEntity>()
                  .ReverseMap();
            this.CreateMap<DoctorDto, DoctorEntity>()
                .ReverseMap();
            this.CreateMap<ContactDto, ContactEntity>()
                .ReverseMap();
            this.CreateMap<AddressDto, AddressEntity>()
                .ReverseMap();
            this.CreateMap<DataSourceDto, DataSourceEntity>()
                .ReverseMap();
        }
    }
}
