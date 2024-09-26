
using MediatR;
using ResumeTemplate.Entities;
using ResumeTemplate.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeTemplate.DTO
{
    public class RequestParameters<T> where T : BaseModel
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }
        public IRepository<T> Repository { get; set; }
        

        public RequestParameters(IMediator mediator,
            UserState userState,
            IRepository<T> repository)
        {
            Mediator = mediator;
            UserState = userState;
            Repository = repository;
        }


    }
}
