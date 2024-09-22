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

    public record IsManagedProjectToOwnerCommand(int ProjectID,int UserCreateID) : IRequest<ResultDTO<bool>>;

    public class IsManagedProjectToOwnerCommandHandler : BaseRequestHandler<UserProject, IsManagedProjectToOwnerCommand, ResultDTO<bool>>
    {

        public IsManagedProjectToOwnerCommandHandler(RequestParameters<UserProject> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(IsManagedProjectToOwnerCommand request, CancellationToken cancellationToken)
        {
            var isManagedProject = await _repository.GetAllAsync()
                                .AnyAsync(
                                    u => u.ProjectID == request.ProjectID
                                    && u.UserID == request.UserCreateID
                                    && u.Role == UserRole.Owner
                                );


            if (!isManagedProject)
            {
                return ResultDTO<bool>.Faliure("User Create ID isn't managed this project!");
            }

            return ResultDTO<bool>.Sucess(true, "User Create ID is already managed this project");
        }

    }
}
