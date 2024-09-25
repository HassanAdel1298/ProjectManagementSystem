using MediatR;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Services;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Helpers
{
    public abstract class BaseRequestHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TEntity : BaseModel
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IRepository<TEntity> _repository;
        protected readonly RabbitMQPublisherService _rabbitMQService;

        public BaseRequestHandler(RequestParameters<TEntity> requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _repository = requestParameters.Repository;
            _rabbitMQService = requestParameters.RabbitMQService;


        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
