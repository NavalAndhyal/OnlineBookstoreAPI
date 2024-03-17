using AutoMapper;
using OnlineBookstoreAPI.Application.IRepository.UserRepo;
using OnlineBookstoreAPI.Domain.Helpers;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.DTO;
using OnlineBookstoreAPI.Domain.Models.Filter;
using OnlineBookstoreAPI.Domain.Models.Result;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(int id)
        {
            var result = await _userRepository.Delete(id);
            return result;
        }

        public async Task<UserDto?> FindById(int Id)
        {
            var result = await _userRepository.FindById(Id);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<IEnumerable<UserDto>?> GetAll()
        {
            var result = await _userRepository.GetAll();
            return _mapper.Map<List<UserDto>>(result);
        }

        public async Task<UserDto?> GetUserDtoForLogin(LoginDto loginDto)
        {
            var result = await _userRepository.GetUserForLogin(_mapper.Map<Login>(loginDto));
            return _mapper.Map<UserDto>(result);
        }

        public async Task<OperationResult<UserDto>?> GetUsers(FilterAndPaginationModel filterAndPaginationModel)
        {
            var query = await _userRepository.GetAll();
            var pagedList = query!.ApplyFilterSortingPagination(filterAndPaginationModel);

            var users = _mapper.Map<List<UserDto>>(pagedList);
            return new OperationResult<UserDto>(pagedList.TotalCount, pagedList.TotalPages, pagedList.CurrentPage, pagedList.hasNext, pagedList.hasPrevious, users);
        }

        public async Task<UserDto?> Insert(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            var result = await _userRepository.Insert(user);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto?> Update(int id, UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            var result = await _userRepository.Update(id,user);
            return _mapper.Map<UserDto>(result);
        }
    }
}
