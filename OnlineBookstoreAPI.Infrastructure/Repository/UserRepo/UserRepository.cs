using Microsoft.EntityFrameworkCore;
using OnlineBookstoreAPI.Application.IRepository.UserRepo;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Infrastructure.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        public UserRepository(BookStoreContext bookStoreContext) 
        {
            _bookStoreContext = bookStoreContext;
        }
        public async Task<bool> Delete(int id)
        {
            var existingUser = await FindById(id);
            if (existingUser != null)
            {
                _bookStoreContext.Users.Remove(existingUser);
                int recordsUpdated = await _bookStoreContext.SaveChangesAsync();
                if (recordsUpdated > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<User?> FindById(int Id)
        {
            var User = await _bookStoreContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == Id);
            return User;
        }

        public async Task<IQueryable<User>?> GetAll()
        {
            return _bookStoreContext.Users.Include(r => r.Role).AsQueryable();
        }

        public async Task<User?> GetUserForLogin(Login login)
        {
            var User = await _bookStoreContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == login.UserName && login.Password == login.Password);
            if(User != null)
                return User;
            return null;
        }

        public async Task<User?> Insert(User entity)
        {
            await _bookStoreContext.Users.AddAsync(entity);
            int recordsInserted = await _bookStoreContext.SaveChangesAsync();
            if(recordsInserted > 0)
            {
                return entity;
            }
            return null;
        }

        public async Task<User?> Update(int id, User entity)
        {
            var existingUser = await FindById(id);
            if(existingUser != null)
            {
                _bookStoreContext.Users.Update(entity);
                int recordsUpdated = await _bookStoreContext.SaveChangesAsync();
                if(recordsUpdated > 0)
                {
                    return entity;
                }
            }
            return null;
        }
    }
}
