using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeTemplate.CQRS.Skills.Commands;
using ResumeTemplate.CQRS.Skills.Orchestrators;
using ResumeTemplate.CQRS.Skills.Queries;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.Helpers;
using ResumeTemplate.ViewModel;
using ResumeTemplate.ViewModel.Skills;

namespace ResumeTemplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillController : BaseController
    {
        public SkillController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<SkillCreateDTO>> CreateSkill(SkillCreateViewModel skillViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<SkillCreateDTO>.Faliure("User isn't Login");
            }

            var skillDTO = skillViewModel.MapOne<SkillCreateDTO>();

            skillDTO.UserID = userID;

            var resultDTO = await _mediator.Send(new CreateSkillOrchestrator(skillDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<SkillCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<SkillCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<SkillReturnViewDTO>>> GetAllSkills(ViewSkillViewModel viewSkillViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<SkillReturnViewDTO>>.Faliure("User isn't Login");
            }

            var skillViewDTO = viewSkillViewModel.MapOne<SkillViewDTO>();

            skillViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllSkillsQuery(skillViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<SkillReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<SkillReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<SkillReturnViewDTO>>> GetAllSkillsByUser(ViewSkillViewModel viewSkillViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<SkillReturnViewDTO>>.Faliure("User isn't Login");
            }

            var skillViewDTO = viewSkillViewModel.MapOne<SkillViewDTO>();

            skillViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllSkillsByUserQuery(skillViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<SkillReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<SkillReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        
        [HttpDelete]
        [Authorize]
        public async Task<ResultViewModel<bool>> DeleteSkillForUser(int skillID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            SkillDeleteDTO skillDeleteDTO = new SkillDeleteDTO()
            {
                SkillID = skillID,
                UserID = userID
            };

            var resultDTO = await _mediator.Send(new DeleteSkillForUserCommand(skillDeleteDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }



    }
}
