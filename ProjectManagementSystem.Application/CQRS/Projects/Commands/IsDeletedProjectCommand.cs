using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Projects.Commands
{

    public record IsDeletedProjectCommand(int ProjectID) : IRequest<ResultDTO<bool>>;

    public class IsDeletedProjectCommandHandler : BaseRequestHandler<Project, IsDeletedProjectCommand, ResultDTO<bool>>
    {

        public IsDeletedProjectCommandHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(IsDeletedProjectCommand request, CancellationToken cancellationToken)
        {
            var isVerifiedProject = await _repository.GetAllAsync()
                                .AnyAsync(u => u.ID == request.ProjectID);


            if (!isVerifiedProject)
            {
                return ResultDTO<bool>.Faliure("Project ID Not Found");
            }

            return ResultDTO<bool>.Sucess(true, "Project ID Found");
        }

    }
}
