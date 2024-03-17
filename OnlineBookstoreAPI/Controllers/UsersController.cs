using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreAPI.Application.Service.UserService;
using OnlineBookstoreAPI.Domain.Models.DTO;
using OnlineBookstoreAPI.Domain.Models.Filter;
using OnlineBookstoreAPI.Domain.Models.Result;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBookstoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles ="Admin")]
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserDto>?> Get()
        {
            return await _userService.GetAll();
        }
        [Authorize(Roles = "Admin")]
        // GET: api/<UsersController>
        [HttpPost("GetUsersWithFilter")]
        public async Task<OperationResult<UserDto>?> GetUsersWithFilter([FromBody] FilterAndPaginationModel filterAndPaginationModel)
        {
            return await _userService.GetUsers(filterAndPaginationModel);
        }
        [Authorize(Roles = "Admin,User")]

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDto?> Get(int id)
        {
            return await _userService.FindById(id);
        }

        [Authorize(Roles = "Admin,User")]

        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserDto?> Post([FromBody] UserDto userDto)
        {
            return await _userService.Insert(userDto);
        }

        [Authorize(Roles = "Admin,User")]

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<UserDto?> Put(int id, [FromBody] UserDto userDto)
        {
            return await _userService.Update(id,userDto);

        }

        [Authorize(Roles = "Admin")]
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await (_userService.Delete(id));
        }
    }
}
