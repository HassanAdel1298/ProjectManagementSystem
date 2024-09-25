using MediatR;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Queries
{

    public record ViewAllTasksQuery(TaskViewDTO taskViewDTO) : IRequest<ResultDTO<IEnumerable<TaskReturnViewDTO>>>;


    public class ViewAllTasksQueryHandler : BaseRequestHandler<AppTask, ViewAllTasksQuery, ResultDTO<IEnumerable<TaskReturnViewDTO>>>
    {

        public ViewAllTasksQueryHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<TaskReturnViewDTO>>> Handle(ViewAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasksDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.taskViewDTO.pageNumber,
                                            request.taskViewDTO.pageSize
                                        )
                                        .Where(
                                            t => (t.UserCreateID == request.taskViewDTO.userID
                                            || t.UserAssignID == request.taskViewDTO.userID)
                                            && !t.Project.IsDeleted
                                        )
                                        .Select(t => new TaskReturnViewDTO()
                                        {
                                            Title = t.Title,
                                            Description = t.Description,
                                            Status = t.Status,
                                            CreatedDate = t.CreatedDate,
                                            AssignedDate = t.AssignedDate,
                                            ProjectName = t.Project.Title,
                                            UserCreateName = t.UserCreate.FullName,
                                            UserAssignName = t.UserAssign.FullName
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<TaskReturnViewDTO>>.Sucess(tasksDTO, "View Tasks successfully!");
        }
    }
}
