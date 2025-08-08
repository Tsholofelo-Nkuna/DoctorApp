
using DoctorManagement.Presentation.Enums;
using DoctorManagement.Presentation.ViewModels.Base;

namespace DoctorManagement.Presentation.ViewModels
{
    public class FormInputViewModel<TDto> : BaseViewModel<TDto> where TDto: new()
    {
        public string BoundPropertyName { get; set; } = string.Empty;
        public FormInputType InputType {  get; set; } = FormInputType.InputText;
        public string CssColClass { get; set; } = "col-12";
        public string? InputTextType { get; set; }
    }
}
