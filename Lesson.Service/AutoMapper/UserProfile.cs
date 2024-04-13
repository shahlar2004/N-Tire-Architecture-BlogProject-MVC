using AutoMapper;
using Lesson.Entity.DTOs.Users;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
           CreateMap<UserDTO,AppUser>().ReverseMap();
           CreateMap<UserAddDTO,AppUser>().ReverseMap();
           CreateMap<UserUpdateDTO,AppUser>().ReverseMap();
           CreateMap<UserProfileDTO,AppUser>().ReverseMap();   
        }
    }
}
