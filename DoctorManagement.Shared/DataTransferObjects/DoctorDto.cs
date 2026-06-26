using DoctorManagement.Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class DoctorDto: DtoBase
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PracticeNumber { get; set; } = string.Empty;
       
        [Required(ErrorMessage = "Specialty is required")]
        public int SpecialtyValue { get; set; }
        public AddressDto? PracticeSite { get; set; } 
        public ContactDto? Contact { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string TitleDescription { get; set; } = string.Empty;
        [Range(1, int.MaxValue)]
        public int TitleDatasourceId { get; set; }
        public DataSourceDto? Title { get; set; }

        public DataSourceDto? Specialty { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool AcceptHomeVisits { get; set; }

        public string ConsultationFeeDisplay => $"R{(ConsultationFee*1.1m):F2}";
        public decimal NumericDisplayedConsultationFee => ConsultationFee * 1.1m;
        public string Initials
        {
            get
            {
                return (this.FirstName?.Any() ?? false) ? $"{this.FirstName?[0]}" : string.Empty;
            }
        }
        public string DisplayName => $"{Title?.Description} {this.Initials} {this.LastName}";
        public string PhotoFileName { get; set; } = string.Empty;
        public string PhotoMimeType { get; set; } = string.Empty;
        public byte[] PhotoContents { get; set; } = [];
        public string PhotoUrl => $"data:{PhotoMimeType};base64,{Convert.ToBase64String(PhotoContents)}";
    }
}
