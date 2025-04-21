using DoctorManagement.Shared.DataTransferObjects.Base;
using DoctorManagement.Presentation.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels
{
    public class CardComponentViewModel<TDto> : BaseViewModel<TDto> where TDto : new()
    {
        public string Title { get; set; } = string.Empty;
    }
}
