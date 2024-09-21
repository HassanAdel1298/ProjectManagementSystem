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

    public record SearchTasksByNameQuery(TaskSearchDTO taskSearchDTO) : IRequest<ResultDTO<IEnumerable<TaskReturnViewDTO>>>;



    public class SearchTasksByNameQueryHandler : BaseRequestHandler<AppTask, SearchTasksByNameQuery, ResultDTO<IEnumerable<TaskReturnViewDTO>>>
    {

        public SearchTasksByNameQueryHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<TaskReturnViewDTO>>> Handle(SearchTasksByNameQuery request, CancellationToken cancellationToken)
        {
            var tasksDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.taskSearchDTO.pageNumber,
                                            request.taskSearchDTO.pageSize
                                        )
                                        .Where(t => t.Title == request.taskSearchDTO.Name 
                                                && (t.UserCreateID == request.taskSearchDTO.userID
                                                || t.UserAssignID == request.taskSearchDTO.userID)
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


            return ResultDTO<IEnumerable<TaskReturnViewDTO>>.Sucess(tasksDTO, "View Tasks by Name successfully!");
        }
    }
}
