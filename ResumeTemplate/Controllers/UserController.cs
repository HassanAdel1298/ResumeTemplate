using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeTemplate.DTO.Users;
using ResumeTemplate.ViewModel.Users;
using ResumeTemplate.DTO;
using ResumeTemplate.Helpers;
using ResumeTemplate.ViewModel;
using ResumeTemplate.CQRS.Users.Commands;
using ResumeTemplate.CQRS.Users.Orchestrators;

namespace ResumeTemplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        public async Task<ResultViewModel<RegisterUserDTO>> Register(RegisterViewModel registerViewModel)
        {
            var registerUserDTO = registerViewModel.MapOne<RegisterUserDTO>();

            var resultDTO = await _mediator.Send(new RegisterUserOrchestrator(registerUserDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<RegisterUserDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<RegisterUserDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel<string>> Login(LoginViewModel loginViewModel)
        {
            var loginUserDTO = loginViewModel.MapOne<LoginUserDTO>();

            var resultDTO = await _mediator.Send(new LoginUserCommand(loginUserDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<string>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<string>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        public async Task<ResultViewModel<bool>> VerifyAccount(VerifyAccountViewModel verifyAccountViewModel)
        {
            var verifyAccountDTO = verifyAccountViewModel.MapOne<VerifyUserDTO>();

            var resultDTO = await _mediator.Send(new VerifyAccountCommand(verifyAccountDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }



    }
}
