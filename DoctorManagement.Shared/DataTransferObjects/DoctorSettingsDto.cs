
using DoctorManagement.Shared.DataTransferObjects.Base;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagement.Shared.DataTransferObjects
{
    public class DoctorSettingsDto : DtoBase
    {
        [Display(Name = "Consultation Fee")]
        public decimal ConsultationFee { get; set; }
    }
}
