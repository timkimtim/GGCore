using AutoMapper;
using GGCore.DTOs;
using GGCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Configs
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
        }
    }
}
