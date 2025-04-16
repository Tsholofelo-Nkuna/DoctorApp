using DoctorManagement.BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.BusinessLogicLayer.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor) {
          this._userManager = userManager;
          this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityUser?> GetCurrentUserAsync()
        {
          
           if(this._httpContextAccessor.HttpContext.User is ClaimsPrincipal user)
            {
                return  await _userManager.GetUserAsync(user);
            }
           return null;
        }
    }
}
