using static System.Runtime.InteropServices.JavaScript.JSType;
using WebApplicationcrud.Models.Entities;
using AutoMapper;
using WebApplicationcrud.Models;


    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();
        }
    }


