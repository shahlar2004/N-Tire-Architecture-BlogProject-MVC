using AutoMapper;
using FluentValidation;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Users;
using Lesson.Entity.Entities;
using Lesson.Entity.Enums;
using Lesson.Service.Extensions;
using Lesson.Service.Helpers.Images;
using Lesson.Service.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Services.Concrete
{
    public class UserService : IUsersService
    {
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IValidator<AppUser> validator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContext;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IImageHelper imageHelper;
        private readonly ClaimsPrincipal _user;

        public UserService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IValidator<AppUser> validator, IUnitOfWork unitOfWork, IHttpContextAccessor httpContext, SignInManager<AppUser> signInManager, IImageHelper imageHelper)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.validator = validator;
            this.unitOfWork = unitOfWork;
            this.httpContext = httpContext;
            this.signInManager = signInManager;
            this.imageHelper = imageHelper;
            _user = httpContext.HttpContext.User;
            
        }


        public async Task<IdentityResult> CreateUser(UserAddDTO userAddDTO, AppUser map)
        {
            var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDTO.Password) ? "" : userAddDTO.Password);

            if (result.Succeeded)
            {
                var findRole = await roleManager.FindByIdAsync(userAddDTO.RoleId.ToString());
                await userManager.AddToRoleAsync(map,findRole.ToString());
            }

            return result;
        }



   

        public async Task<AppUser> FindUserAsync(string Id)
        {
            var findUser = await userManager.FindByIdAsync(Id);
            return findUser;
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserDTO>> GetAllRolesWithUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserDTO>>(users);
            foreach (var user in map)
            {
                var findUser = await userManager.FindByIdAsync(user.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));
                user.Role = role;

            }

            return map;
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            var image = await unitOfWork.GetRepository<Image>().GetByGuidAsync(user.ImageId);
            var map = mapper.Map<UserProfileDTO>(user);
            map.image = image;
            return map;
        }

 
        public async Task<UserProfileDTO> GetUserProfileAsync()
        {
            var user = await userManager.GetUserAsync(_user);
            var getImage = await unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == user.Id, x => x.Image);
            var map = mapper.Map<UserProfileDTO>(getImage);
            return map;
        }

        public async Task<IdentityResult> UpdateUser(UserUpdateDTO userUpdateDTO)
        {


            var user = await userManager.FindByIdAsync(userUpdateDTO.Id.ToString());
            var validResult = await validator.ValidateAsync(user);
            var userRole = string.Join("", await userManager.GetRolesAsync(user));
            user.UserName = userUpdateDTO.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();

            mapper.Map(userUpdateDTO, user);
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await roleManager.FindByIdAsync(userUpdateDTO.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);
            }

            return (result);


        }

  
        //public async Task<IdentityResult> UpdateUserProfileAsync(UserProfileDTO userProfileDTO)
        //{
        //    var user = await userManager.GetUserAsync(_user);
        //    var imageuser = await unitOfWork.GetRepository<Image>().GetByGuidAsync(user.ImageId);
        //    user.Image = imageuser;
        //    var UserEmail = _user.GetLoggedInEmail();
        //}

        async Task<(IdentityResult, string)> IUsersService.DeleteUserAsync(string userId)
        {
            var userObj = await userManager.FindByIdAsync(userId.ToString());
            var result = await userManager.DeleteAsync(userObj);
            return (result, userObj.Email);
        }





        //public async Task<string> GetRoleAsync(AppUser appUser)
        //{
        //    var role= string.Join("", await userManager.GetRolesAsync(appUser));
        //    return role;
        //}


        private async Task<Guid> UploadImageForUser(UserProfileDTO userProfileDto)
        {
            var userEmail = _user.GetLoggedInEmail();

            var imageUpload = await imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageType.User);
            Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);

            return image.Id;
        }




        public async Task<bool> UserProfileUpdateAsync(UserProfileDTO userProfileDto)
        {
            var userId = _user.GetLoggedInUserId();
            var user = await unitOfWork.GetRepository<AppUser>().GetByGuidAsync(userId);
            var myImg = user.ImageId;

            if (userProfileDto.CurrentPassword == null)
            {
                return false;
            }

            var isVerified = await userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);

            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                    mapper.Map(userProfileDto, user);

                    if (userProfileDto.Photo != null)
                        user.ImageId = await UploadImageForUser(userProfileDto);

                    else
                    {
                        user.ImageId = myImg;
                    }
                    await userManager.UpdateAsync(user);
                    await unitOfWork.SaveAsync();

                    return true;
                }
                else
                    return false;
            }
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(user);
                mapper.Map(userProfileDto, user);

                if (userProfileDto.Photo != null)
                    user.ImageId = await UploadImageForUser(userProfileDto);
                else
                {
                    user.ImageId = myImg;
                }

                await userManager.UpdateAsync(user);
                await unitOfWork.SaveAsync();
                return true;
            }

            else
                return false;
        }

    }
}
