using DoctorManagement.API.Controllers.Base;
using DoctorManagement.BusinessLogicLayer.Interfaces;
using DoctorManagement.Shared.DataTransferObjects;
using DoctorManagement.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DoctorManagement.Shared.Models;

namespace DoctorManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController: ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDataSourceService _dataSourceService;
        private readonly HttpClient AppApi;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(IUserContextService userContextService,
            IHttpClientFactory httpClientFactory,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IDataSourceService dataSourceService, ILogger<AccountsController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dataSourceService = dataSourceService;
            this.AppApi = httpClientFactory.CreateClient(WebApiNameConstants.AppApi);
            _logger = logger;
        }

        [HttpPost("Signup/Patient")]
        public async Task<ResponseDto<IEnumerable<Guid>>> PatientSignup(SignupDto patientSignUp)
        {
            try
            {
                
                var userCreationResult = await _userManager.CreateAsync(new()
                {
                    UserName = patientSignUp.Credentials.Username,
                    Email = patientSignUp.Credentials.Username,
                    PhoneNumber = patientSignUp.Contact.Phone
                },
                   patientSignUp.Credentials.Password
                   );
                if (userCreationResult is { Succeeded: true }
                && (_userManager.Users.FirstOrDefault(x => x.UserName == patientSignUp.Credentials.Username)) is IdentityUser newlyCreatedUser)
                {
                    //ToDo: Verify email
                    if (!_roleManager.Roles.Any(x => x.Name == RoleConstants.Patient))
                    {
                        await _roleManager.CreateAsync(new() { Name = RoleConstants.Patient });
                    }
                    //move this logic to patient service
                    var userRoleCreated = await _userManager.AddToRoleAsync(newlyCreatedUser, RoleConstants.Patient);
                    if (userRoleCreated is { Succeeded: true })
                    {
                        _logger.LogInformation($"Posting to patient endpoint. patient uploaded photo of size {patientSignUp.PhotoContents.Length} bytes");
                        var apiResponse = await this.AppApi.PostAsJsonAsync("api/Patients", new PatientDto
                        {
                            Address = patientSignUp.Address,
                            Contact = patientSignUp.Contact,
                            UserId = newlyCreatedUser.Id,
                            IdentityNumber = patientSignUp.GeneralInfo.IdentityNumber,
                            MedicalAidNumber = patientSignUp.GeneralInfo.MedicalAidNumber,
                            MedicalAidPlanName = patientSignUp.GeneralInfo.MedicalAidPlanName,
                            MedicalAidProvider = patientSignUp.GeneralInfo.MedicalAidProvider,
                            PhotoContents = patientSignUp.PhotoContents,
                            PhotoFileName = patientSignUp.PhotoFileName,
                            PhotoMimeType = patientSignUp.PhotoMimeType
                        });
                        if (apiResponse is { IsSuccessStatusCode: true }
                        && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<IEnumerable<Guid>>>()) is ResponseDto<IEnumerable<Guid>> validResponseContent)
                        {
                            return validResponseContent;
                        }
                        else
                        {
                            return new()
                            {
                                Data = [],
                                Message = "Failed to create patient record!"
                            };
                        }

                    }
                    else
                    {
                        return new()
                        {
                            Data = [],
                            Message = userRoleCreated?.Errors?.FirstOrDefault()?.Description ?? string.Empty,
                        };
                    }
                }
                else
                {
                    return new()
                    {
                        Data = [],
                        Message = userCreationResult?.Errors?.FirstOrDefault()?.Description ?? string.Empty,

                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new()
                {
                    Data = [],
                    Message = ex.Message,

                };
            }
        }

        [HttpPost("Signup/Doctor")]
        public async Task<ResponseDto<IEnumerable<Guid>>> DoctorSignup(SignupDoctorDto doctorSignup)
        {
            var userCreationResult = await _userManager.CreateAsync(new()
            {
                UserName = doctorSignup.Credentials.Username,
                Email = doctorSignup.Credentials.Username,
                PhoneNumber = doctorSignup.Contact?.Phone
            },
               doctorSignup.Credentials.Password
               );
            if (userCreationResult is { Succeeded: true }
            && (_userManager.Users.FirstOrDefault(x => x.UserName == doctorSignup.Credentials.Username)) is IdentityUser newlyCreatedUser)
            {
                //ToDo: Verify email
                if (!_roleManager.Roles.Any(x => x.Name == RoleConstants.Doctor))
                {
                    await _roleManager.CreateAsync(new() { Name = RoleConstants.Doctor });
                }
                //move this to doctor service
                var userRoleCreated = await _userManager.AddToRoleAsync(newlyCreatedUser, RoleConstants.Doctor);
                if (userRoleCreated is { Succeeded: true })
                {
                    var specialtyRecord = _dataSourceService.Get(new PageRequestDto<DataSourceFilter>() { Filter = new() { TypeCode = DataSourceTypeCodeConstants.Specialty }, GetAllPages = true })
                        .Data?.FirstOrDefault(x => x.Value == doctorSignup.SpecialtyValue);
                    var apiResponse = await this.AppApi.PostAsJsonAsync<DoctorDto>("api/Doctors", new()
                    {
                        Contact = doctorSignup.Contact,
                        PracticeNumber = doctorSignup.PracticeNumber,
                        PracticeSite = doctorSignup.PracticeSite,
                        Specialty = specialtyRecord,
                        FirstName = doctorSignup.FirstName,
                        LastName = doctorSignup.LastName,
                        TitleDatasourceId = doctorSignup.TitleDatasourceId,
                        TitleDescription = doctorSignup.TitleDescription,
                        UserId = newlyCreatedUser.Id,
                    });
                  
                    if (apiResponse is { IsSuccessStatusCode: true }
                    && (await apiResponse.Content.ReadFromJsonAsync<ResponseDto<IEnumerable<Guid>>>()) is ResponseDto<IEnumerable<Guid>> validResponseContent)
                    {
                        return validResponseContent;
                    }
                    else
                    {
                        return new()
                        {
                            Data = [],
                            Message = "Failed to create doctor record"
                        };
                    }
                   
                }
                else
                {
                    return new()
                    {
                        Data = [],
                        Message = userRoleCreated?.Errors?.FirstOrDefault()?.Description ?? string.Empty,
                    };
                }
            }
            else
            {
                return new()
                {
                    Data = [],
                    Message = userCreationResult?.Errors?.FirstOrDefault()?.Description ?? string.Empty,

                };
            }
        }

        [HttpPost("[action]")]
        public async Task<ResponseDto<TokenResponseDto>> SignIn(LoginCredentialsDto credentials)
        {
            var response = await this.AppApi.PostAsJsonAsync<Dictionary<string, string>>(
               "/login",
                 new()
                 {
                      { "email", credentials.Username },
                      { "password", credentials.Password }
                 });
            var tkn = (await response.Content.ReadFromJsonAsync<TokenResponseDto>());
            if (response is { IsSuccessStatusCode: true } && tkn is TokenResponseDto token)
            { 
                return new()
                {
                    Data = token,
                    Message = "Login successful"
                };
            }
            else
            {
                return new()
                {
                    Message = "Login failed"
                };
            }
        }
    }
}
