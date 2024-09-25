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
using ProjectManagementSystem.Application.DTO.TaskTimers;
using ProjectManagementSystem.Entity.Migrations;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.TaskTimers.Queries
{

    public record ViewAllTaskTimersQuery(TaskTimerViewDTO taskTimerViewDTO) : IRequest<ResultDTO<IEnumerable<TaskTimerReturnViewDTO>>>;


    public class ViewAllTaskTimersQueryHandler : BaseRequestHandler<TaskTimer, ViewAllTaskTimersQuery, ResultDTO<IEnumerable<TaskTimerReturnViewDTO>>>
    {

        public ViewAllTaskTimersQueryHandler(RequestParameters<TaskTimer> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<TaskTimerReturnViewDTO>>> Handle(ViewAllTaskTimersQuery request, CancellationToken cancellationToken)
        {
            var taskTimersDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.taskTimerViewDTO.pageNumber,
                                            request.taskTimerViewDTO.pageSize
                                        )
                                        .Where(
                                            tt => (tt.Task.UserCreateID == request.taskTimerViewDTO.userID
                                            || tt.Task.UserAssignID == request.taskTimerViewDTO.userID)
                                            && !tt.Task.IsDeleted
                                            && tt.TaskID == request.taskTimerViewDTO.TaskID
                                        )
                                        .Select(tt => new TaskTimerReturnViewDTO()
                                        {
                                            Title = tt.Title,
                                            DurationByMinute = tt.DurationByMinute,
                                            TaskName = tt.Task.Title
                                        }).ToListAsync();

            if (taskTimersDTO is null)
            {
                return ResultDTO<IEnumerable<TaskTimerReturnViewDTO>>.Faliure("User isn't managed this Task");
            }


            return ResultDTO<IEnumerable<TaskTimerReturnViewDTO>>.Sucess(taskTimersDTO, "View Tasks successfully!");
        }
    }
}
