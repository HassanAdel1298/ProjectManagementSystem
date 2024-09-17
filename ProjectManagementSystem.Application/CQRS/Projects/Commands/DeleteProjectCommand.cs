using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.CQRS.Projects.Queries;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Projects.Commands
{

    public record DeleteProjectCommand(int projectID) : IRequest<ResultDTO<bool>>;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResultDTO<bool>>
    {
        IRepository<Project> _repository;
        IMediator _mediator;

        public DeleteProjectCommandHandler(IRepository<Project> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetAllAsync()
                                        .Where( p => p.ID == request.projectID)
                                        .FirstOrDefaultAsync();

            if (project is null)
            {
                return ResultDTO<bool>.Faliure("Project ID Not Found!");
            }

            _repository.DeleteAsync(project);

            return ResultDTO<bool>.Sucess(true, "Delete Project successfully!");
        }
    }
}
