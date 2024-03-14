using OnlineBookstoreAPI.Application.IRepository.Base;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.IRepository.UserRepo
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User?> GetUserForLogin(Login login);
    }
}
