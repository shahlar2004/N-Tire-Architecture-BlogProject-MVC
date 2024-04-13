using Lesson.Entity.DTOs.Users;
using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Abstraction
{
    public interface IUsersService
    {


        Task<List<AppRole>> GetAllRolesAsync();
        Task <List<UserDTO>> GetAllRolesWithUsersAsync();    
        Task <IdentityResult> CreateUser(UserAddDTO userAddDTO, AppUser map);

        //Task<string> GetRoleAsync(AppUser appUser);
        Task<AppUser> FindUserAsync(string Id);

        Task <IdentityResult> UpdateUser(UserUpdateDTO userUpdateDTO);

        Task<(IdentityResult,string)> DeleteUserAsync(string userId);
        Task<UserProfileDTO> GetUserProfileAsync();

       //Task<bool> UserProfileUpdateAsync(UserProfileDTO userProfileDto);

      //  Task<IdentityResult> UpdateUserProfileAsync(UserProfileDTO userProfileDTO);

        Task<bool> UserProfileUpdateAsync(UserProfileDTO userProfileDto);

       // Task<bool> UserProfileUpdateAsync(UserProfileDTO userProfileDto);
    }
}
