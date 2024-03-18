using OnlineBookstoreAPI.Application.IRepository.Base;
using OnlineBookstoreAPI.Domain.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.IRepository.BookRepo
{
    public interface IBookRepository : IBaseRepository<Book,string>
    {
    //    Task<IQueryable<Book>?> GetAll();
    //    Task<Book?> FindById(string Id);
    //    Task<Book?> Insert(Book entity);
    //    Task<Book?> Update(string id, Book entity);
    //    Task<bool> Delete(string id);
    }
}
