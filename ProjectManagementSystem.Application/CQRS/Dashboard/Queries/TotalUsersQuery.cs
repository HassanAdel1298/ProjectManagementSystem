using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Dashboard;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Dashboard.Queries
{

    public record TotalUsersQuery() : IRequest<ResultDTO<TotalUsersDTO>>;


    public class TotalUsersQueryHandler : BaseRequestHandler<User, TotalUsersQuery, ResultDTO<TotalUsersDTO>>
    {

        public TotalUsersQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TotalUsersDTO>> Handle(TotalUsersQuery request, CancellationToken cancellationToken)
        {
            var TotalActive = await _repository.GetAllAsync()
                                                .Where(u => u.IsEmailVerified)
                                                .CountAsync(u => u.IsActive);
            
            var TotalInactive = await _repository.GetAllAsync()
                                                .Where(u => u.IsEmailVerified)
                                                .CountAsync(u => !u.IsActive);

            TotalUsersDTO totalUsersDTO = new TotalUsersDTO()
            {
                Active = TotalActive,
                Inactive = TotalInactive
            };

            return ResultDTO<TotalUsersDTO>.Sucess(totalUsersDTO, "Total Users successfully!");
        }
    }
}
