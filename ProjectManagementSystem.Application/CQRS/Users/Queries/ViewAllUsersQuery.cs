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
using ProjectManagementSystem.Application.DTO.Users;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.Users.Queries
{
  
    public record ViewAllUsersQuery(UsersViewDTO userViewDTO) : IRequest<ResultDTO<IEnumerable<UsersReturnViewDTO>>>;


    public class ViewAllUsersQueryHandler : BaseRequestHandler<User, ViewAllUsersQuery, ResultDTO<IEnumerable<UsersReturnViewDTO>>>
    {

        public ViewAllUsersQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<UsersReturnViewDTO>>> Handle(ViewAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.userViewDTO.pageNumber,
                                            request.userViewDTO.pageSize
                                        )
                                        .Select(u => new UsersReturnViewDTO()
                                        {
                                            FirstName = u.FirstName,
                                            LastName = u.LastName,
                                            FullName = u.FullName,
                                            Email = u.Email,
                                            UserName = u.UserName,
                                            PhoneNumber = u.PhoneNumber,
                                            Country = u.Country,
                                            IsActive = u.IsActive,
                                            CreatedDate = u.CreatedDate
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<UsersReturnViewDTO>>.Sucess(usersDTO, "View Users successfully!");
        }
    }
}
