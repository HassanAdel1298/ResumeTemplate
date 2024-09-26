﻿
using MediatR;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeTemplate.Helpers
{
    public abstract class BaseRequestHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TEntity : BaseModel
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IRepository<TEntity> _repository;

        public BaseRequestHandler(RequestParameters<TEntity> requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _repository = requestParameters.Repository;

        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
