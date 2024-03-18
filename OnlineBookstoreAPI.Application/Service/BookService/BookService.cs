using AutoMapper;
using OnlineBookstoreAPI.Application.IRepository.BookRepo;
using OnlineBookstoreAPI.Application.IRepository.UserRepo;
using OnlineBookstoreAPI.Domain.Helpers;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.Books;
using OnlineBookstoreAPI.Domain.Models.DTO;
using OnlineBookstoreAPI.Domain.Models.Filter;
using OnlineBookstoreAPI.Domain.Models.Result;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(string id)
        {
            var result = await _bookRepository.Delete(id);
            return result;
        }

        public async Task<BookDto?> FindById(string Id)
        {
            var result = await _bookRepository.FindById(Id);
            return _mapper.Map<BookDto>(result);
        }

        public async Task<IEnumerable<BookDto>?> GetAll()
        {
            var result = await _bookRepository.GetAll();
            return _mapper.Map<List<BookDto>>(result!.Take(100).ToList());
        }

        public async Task<OperationResult<BookDto>?> GetBooks(FilterAndPaginationModel filterAndPaginationModel)
        {
            var query = await _bookRepository.GetAll();
            var pagedList = query!.ApplyFilterSortingPagination(filterAndPaginationModel);

            var books = _mapper.Map<List<BookDto>>(pagedList);
            return new OperationResult<BookDto>(pagedList.TotalCount, pagedList.TotalPages, pagedList.CurrentPage, pagedList.hasNext, pagedList.hasPrevious, books);
        }

        public async Task<BookDto?> Insert(BookDto entity)
        {
            var book = _mapper.Map<Book>(entity);
            var result = await _bookRepository.Insert(book);
            return _mapper.Map<BookDto>(result);
        }

        public async Task<BookDto?> Update(string id, BookDto entity)
        {
            var book = _mapper.Map<Book>(entity);
            var result = await _bookRepository.Update(id,book);
            return _mapper.Map<BookDto>(result);
        }
    }
}
