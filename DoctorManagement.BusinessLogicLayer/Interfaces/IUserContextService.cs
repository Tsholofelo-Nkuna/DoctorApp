using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Interfaces
{
    public interface IUserContextService
    {
        public Task<IdentityUser?> GetCurrentUserAsync();
        public Task<IEnumerable<string>> GetCurrentUserRoles();
    }
}
