using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreAPI.Application.Service.BookService;
using OnlineBookstoreAPI.Application.Service.UserService;
using OnlineBookstoreAPI.Domain.Models.DTO;
using OnlineBookstoreAPI.Domain.Models.Filter;
using OnlineBookstoreAPI.Domain.Models.Result;

namespace OnlineBookstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        //[Authorize(Roles = "Admin")]
        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<BookDto>?> Get()
        {
            return await _bookService.GetAll();
        }
        //[Authorize(Roles = "Admin")]
        // GET: api/<BooksController>
        [HttpPost("GetBooksWithFilter")]
        public async Task<OperationResult<BookDto>?> GetUsersWithFilter([FromBody] FilterAndPaginationModel filterAndPaginationModel)
        {
            return await _bookService.GetBooks(filterAndPaginationModel);
        }
        //[Authorize(Roles = "Admin,User")]

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<BookDto?> Get(string id)
        {
            return await _bookService.FindById(id);
        }

        //[Authorize(Roles = "Admin,User")]

        // POST api/<BooksController>
        [HttpPost]
        public async Task<BookDto?> Post([FromBody] BookDto bookDto)
        {
            return await _bookService.Insert(bookDto);
        }

        //[Authorize(Roles = "Admin,User")]

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<BookDto?> Put(string id, [FromBody] BookDto bookDto)
        {
            return await _bookService.Update(id, bookDto);

        }

        //[Authorize(Roles = "Admin")]
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await (_bookService.Delete(id));
        }
    }
}
