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

    public record SearchUsersByNameQuery(UsersSearchDTO userSearchDTO) : IRequest<ResultDTO<IEnumerable<UsersReturnViewDTO>>>;



    public class SearchUsersByNameQueryHandler : BaseRequestHandler<User, SearchUsersByNameQuery, ResultDTO<IEnumerable<UsersReturnViewDTO>>>
    {

        public SearchUsersByNameQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<UsersReturnViewDTO>>> Handle(SearchUsersByNameQuery request, CancellationToken cancellationToken)
        {
            var usersDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.userSearchDTO.pageNumber,
                                            request.userSearchDTO.pageSize
                                        )
                                        .Where(u => u.FullName == request.userSearchDTO.Name)
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


            return ResultDTO<IEnumerable<UsersReturnViewDTO>>.Sucess(usersDTO, "View Users by Name successfully!");
        }
    }
}
