using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeTemplate.CQRS.Experiences.Commands;
using ResumeTemplate.CQRS.Experiences.Orchestrators;
using ResumeTemplate.CQRS.Experiences.Queries;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Experiences;
using ResumeTemplate.Helpers;
using ResumeTemplate.ViewModel;
using ResumeTemplate.ViewModel.Educations;
using ResumeTemplate.ViewModel.Experiences;

namespace ResumeTemplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExperienceController : BaseController
    {
        public ExperienceController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<ExperienceCreateDTO>> CreateExperience(ExperienceCreateViewModel experienceViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<ExperienceCreateDTO>.Faliure("User isn't Login");
            }

            var experienceDTO = experienceViewModel.MapOne<ExperienceCreateDTO>();

            experienceDTO.UserID = userID;

            var resultDTO = await _mediator.Send(new CreateExperienceOrchestrator(experienceDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<ExperienceCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<ExperienceCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<ExperienceReturnViewDTO>>> GetAllExperiencesByUser(ViewExperienceViewModel viewExperienceViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<ExperienceReturnViewDTO>>.Faliure("User isn't Login");
            }

            var experienceViewDTO = viewExperienceViewModel.MapOne<ExperienceViewDTO>();

            experienceViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllExperiencesByUserQuery(experienceViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<ExperienceReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<ExperienceReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpDelete]
        [Authorize]
        public async Task<ResultViewModel<bool>> DeleteExperience(int experienceID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            ExperienceDeleteDTO experienceDeleteDTO = new ExperienceDeleteDTO()
            {
                ExperienceID = experienceID,
                UserID = userID
            };

            var resultDTO = await _mediator.Send(new DeleteExperienceCommand(experienceDeleteDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPut]
        [Authorize]
        public async Task<ResultViewModel<ExperienceUpdateDTO>> UpdateExperience(ExperienceUpdateViewModel experienceUpdateViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<ExperienceUpdateDTO>.Faliure("User isn't Login");
            }

            var experienceUpdateDTO = experienceUpdateViewModel.MapOne<ExperienceUpdateDTO>();

            experienceUpdateDTO.UserID = userID;

            var resultDTO = await _mediator.Send(new UpdateExperienceCommand(experienceUpdateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<ExperienceUpdateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<ExperienceUpdateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }


    }
}
