using Microsoft.EntityFrameworkCore;
using OnlineBookstoreAPI.Application.IRepository.Base;
using OnlineBookstoreAPI.Application.IRepository.BookRepo;
using OnlineBookstoreAPI.Application.IRepository.UserRepo;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.Books;
using OnlineBookstoreAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Infrastructure.Repository.BookRepo
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        public BookRepository(BookStoreContext bookStoreContext) 
        {
            _bookStoreContext = bookStoreContext;
        }
        public async Task<bool> Delete(string id)
        {
            var existingBook = await FindById(id);
            if (existingBook != null)
            {
                _bookStoreContext.Books.Remove(existingBook);
                int recordsUpdated = await _bookStoreContext.SaveChangesAsync();
                if (recordsUpdated > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Book?> FindById(string Id)
        {
            var Book = await _bookStoreContext.Books.FirstOrDefaultAsync(u => u.BookId == Id);
            return Book;
        }

        public async Task<IQueryable<Book>?> GetAll()
        {
            return _bookStoreContext.Books.AsQueryable();
        }

        public async Task<Book?> Insert(Book entity)
        {
            await _bookStoreContext.Books.AddAsync(entity);
            int recordsInserted = await _bookStoreContext.SaveChangesAsync();
            if (recordsInserted > 0)
            {
                return entity;
            }
            return null;
        }

        public async Task<Book?> Update(string id, Book entity)
        {
            var existingBook = await FindById(id);
            if(existingBook != null)
            {
                _bookStoreContext.Books.Update(entity);
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
