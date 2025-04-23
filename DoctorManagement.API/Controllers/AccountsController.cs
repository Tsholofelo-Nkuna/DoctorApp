using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountsController(IUserContextService userContextService, IHttpClientFactory httpClientFactory, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) : base(userContextService, httpClientFactory)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("Signup/Patient")]
        public async Task<ResponseDto<bool>> PatientSignup(SignupDto patientSignUp)
        {
           var userCreationResult = await _userManager.CreateAsync(new() { 
               UserName = patientSignUp.Credentials.Username, 
               Email = patientSignUp.Credentials.Username, 
               PhoneNumber = patientSignUp.Contact.Phone}, 
               patientSignUp.Credentials.Password
               );
           if(userCreationResult is { Succeeded: true } 
           && (_userManager.Users.FirstOrDefault(x => x.UserName == patientSignUp.Credentials.Username)) is IdentityUser newlyCreatedUser)
            {
                //ToDo: Verify email
                if(!_roleManager.Roles.Any(x => x.Name == RoleConstants.Patient))
                {
                    await _roleManager.CreateAsync(new() { Name = RoleConstants.Patient });
                }
                var userRoleCreated = await _userManager.AddToRoleAsync(newlyCreatedUser, RoleConstants.Patient);
                if(userRoleCreated is { Succeeded : true })
                {
                   var apiResponse = await this.AppApi.PostAsJsonAsync("api/Patients", new PatientDto { 
                       Address = patientSignUp.Address, 
                       Contact = patientSignUp.Contact,
                       UserId = newlyCreatedUser.Id,
                   });
                   if(apiResponse is { IsSuccessStatusCode : true } 
                   &&  (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<bool>>()) is ResponseDto<bool> validResponseContent)
                    {
                        var patientRecordCreated = validResponseContent.Data;
                    }
                    return new()
                    {
                        Data = true,
                        Message = "Account created successfully!"
                    };
                }
                else
                {
                    return new()
                    {
                        Data = false,
                        Message = userRoleCreated?.Errors?.FirstOrDefault()?.Description ?? string.Empty,
                    };
                }
            }
            else
            {
                return new()
                {
                    Data = false,
                    Message = userCreationResult?.Errors?.FirstOrDefault()?.Description ?? string.Empty,

                };
            }
        }

        //[HttpPost("Signup/Doctor")]
        //public async Task<ResponseDto<bool>> DoctorSignup(SignupDoctorDto patientSignUp)
        //{
        //   await Task.CompletedTask;
        //}
    }
}
