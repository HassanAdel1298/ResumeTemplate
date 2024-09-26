using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeTemplate.DTO
{
    public class ControllereParameters
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }

        public ControllereParameters(IMediator mediator, UserState userState)
        {
            Mediator = mediator;
            UserState = userState;
        }
    }
}
