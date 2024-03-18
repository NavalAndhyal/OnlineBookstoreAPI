using OnlineBookstoreAPI.Application.Service.Base;
using OnlineBookstoreAPI.Domain.Models.DTO;
using OnlineBookstoreAPI.Domain.Models.Filter;
using OnlineBookstoreAPI.Domain.Models.Result;

namespace OnlineBookstoreAPI.Application.Service.BookService
{
    public interface IBookService : IBaseService<BookDto,string>
    {
        public Task<OperationResult<BookDto>?> GetBooks(FilterAndPaginationModel filterAndPaginationModel);

    }
}
