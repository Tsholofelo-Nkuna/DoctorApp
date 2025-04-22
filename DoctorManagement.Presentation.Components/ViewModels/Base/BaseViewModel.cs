using DoctorManagement.Presentation.Interface.Base;
using DoctorManagement.Shared.DataTransferObjects.Base;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Presentation.ViewModels.Base
{
    public class BaseViewModel<TDto> : IBaseViewModel<TDto> where TDto: new()
    {
        private EditContext _editContext;
        private TDto _dto  = new();

        public BaseViewModel()
        {
            _editContext = new EditContext(_dto);
        }
        public TDto Data
        {
            get { return _dto; }
            set
            {
                _dto = value;
                if(_dto is TDto validDto)
                {
                    _editContext = new(validDto);
                }
               
            }
        }
        public EditContext EditContext { get => _editContext;}
    }
}
