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

namespace ProjectManagementSystem.Application.CQRS.UserProjects.Commands
{
   
    public record IsUsedProjectToUserCommand(int ProjectID, int UserAssignID) : IRequest<ResultDTO<bool>>;

    public class IsUsedProjectToUserCommandHandler : BaseRequestHandler<UserProject, IsUsedProjectToUserCommand, ResultDTO<bool>>
    {

        public IsUsedProjectToUserCommandHandler(RequestParameters<UserProject> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(IsUsedProjectToUserCommand request, CancellationToken cancellationToken)
        {
            var isUsedProject = await _repository.GetAllAsync()
                                .AnyAsync(
                                    u => u.ProjectID == request.ProjectID
                                    && u.UserID == request.UserAssignID
                                );


            if (!isUsedProject)
            {
                return ResultDTO<bool>.Faliure("User Assign ID isn't Used this project!");
            }

            return ResultDTO<bool>.Sucess(true, "User Assign ID is already Used this project");
        }

    }
}
