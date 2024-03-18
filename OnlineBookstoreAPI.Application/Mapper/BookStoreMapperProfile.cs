using AutoMapper;
using OnlineBookstoreAPI.Domain.Models;
using OnlineBookstoreAPI.Domain.Models.Books;
using OnlineBookstoreAPI.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Application.Mapper
{
    public class BookStoreMapperProfile : Profile
    {
        public BookStoreMapperProfile() 
        {
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<RoleDto,Role>().ReverseMap();
            CreateMap<LoginDto,Login>().ReverseMap();
            CreateMap<BookDto,Book>().ReverseMap();

        }
    }
}
