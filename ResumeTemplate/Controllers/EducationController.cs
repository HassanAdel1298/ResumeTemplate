using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeTemplate.CQRS.Educations.Commands;
using ResumeTemplate.CQRS.Educations.Orchestrators;
using ResumeTemplate.CQRS.Educations.Queries;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Educations;
using ResumeTemplate.Helpers;
using ResumeTemplate.ViewModel;
using ResumeTemplate.ViewModel.Educations;

namespace ResumeTemplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EducationController : BaseController
    {
        public EducationController(ControllereParameters controllereParameters): base(controllereParameters)
        {
        }



        [Authorize]
        [HttpPost]

        public async Task<ResultViewModel<EducationCreateDTO>> CreateEducation(EducationCreateViewModel educationViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<EducationCreateDTO>.Faliure("User isn't Login");
            }

            var educationDTO = educationViewModel.MapOne<EducationCreateDTO>();

            educationDTO.UserID = userID;

            var resultDTO = await _mediator.Send(new CreateEducationOrchestrator(educationDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<EducationCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<EducationCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



        [Authorize]
        [HttpPost]
        public async Task<ResultViewModel<IEnumerable<EducationReturnViewDTO>>> GetAllEducationsByUser(ViewEducationViewModel viewEducationViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<EducationReturnViewDTO>>.Faliure("User isn't Login");
            }

            var educationViewDTO = viewEducationViewModel.MapOne<EducationViewDTO>();

            educationViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllEducationsByUserQuery(educationViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<EducationReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<EducationReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [Authorize]
        [HttpDelete]
        public async Task<ResultViewModel<bool>> DeleteEducation(int educationID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            EducationDeleteDTO educationDeleteDTO = new EducationDeleteDTO()
            {
                EducationID = educationID,
                UserID = userID
            };

            var resultDTO = await _mediator.Send(new DeleteEducationCommand(educationDeleteDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [Authorize]
        [HttpPut]
        public async Task<ResultViewModel<EducationUpdateDTO>> UpdateEducation(EducationUpdateViewModel educationUpdateViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<EducationUpdateDTO>.Faliure("User isn't Login");
            }

            var educationUpdateDTO = educationUpdateViewModel.MapOne<EducationUpdateDTO>();

            educationUpdateDTO.UserID = userID;

            var resultDTO = await _mediator.Send(new UpdateEducationCommand(educationUpdateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<EducationUpdateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<EducationUpdateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



    }
}
