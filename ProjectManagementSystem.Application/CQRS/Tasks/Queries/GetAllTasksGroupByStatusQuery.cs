using MediatR;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Queries
{

    public record GetAllTasksGroupByStatusQuery(int projectID) : IRequest<ResultDTO<TaskReturnViewGroupByStatusDTO>>;


    public class GetAllTasksGroupByStatusQueryHandler : BaseRequestHandler<AppTask, GetAllTasksGroupByStatusQuery, ResultDTO<TaskReturnViewGroupByStatusDTO>>
    {

        public GetAllTasksGroupByStatusQueryHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskReturnViewGroupByStatusDTO>> Handle(GetAllTasksGroupByStatusQuery request, CancellationToken cancellationToken)
        {
            var tasksDTO = await _repository.GetAllAsync()
                                        .Where(
                                            t => t.ProjectID == request.projectID
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
            
            var tasksByStatusDTO = new TaskReturnViewGroupByStatusDTO()
            {
                Done = tasksDTO.Where(t => t.Status == Status.Done).ToList(),
                InProgress = tasksDTO.Where(t => t.Status == Status.InProgress).ToList(),
                ToDo = tasksDTO.Where(t => t.Status == Status.ToDo).ToList()
            };


            return ResultDTO<TaskReturnViewGroupByStatusDTO>.Sucess(tasksByStatusDTO, "View Tasks by Project Group by Status successfully!");
        }
    }
}
