using OnlineBookstoreAPI.Application.Service.Base;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.DTO;
using OnlineBookstoreAPI.Domain.Models.Filter;
using OnlineBookstoreAPI.Domain.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.Service.UserService
{
    public interface IUserService : IBaseService<UserDto>
    {
        public Task<UserDto?> GetUserDtoForLogin(LoginDto loginDto);
        public Task<OperationResult<UserDto>?> GetUsers(FilterAndPaginationModel filterAndPaginationModel);

    }
}
