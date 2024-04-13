using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer3.Core.Services;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Articles;
using Lesson.Entity.DTOs.Users;
using Lesson.Entity.Entities;
using Lesson.Entity.Enums;
using Lesson.Service.Extensions;
using Lesson.Service.Helpers.Images;
using Lesson.Service.Services.Abstraction;
using Lesson.Web.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using System.Net.WebSockets;
using System.Security.Claims;
using static Lesson.Web.ResultMessages.Messages;

namespace Lesson.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersService userService;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IValidator<AppUser> validator;
        private readonly ClaimsPrincipal _user;

        public UserController(IUsersService userService, IMapper mapper, IToastNotification toastNotification, IValidator<AppUser> validator)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
            this.validator = validator;
        }



        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllRolesWithUsersAsync();
            return View(users);
        }


        [HttpGet]
        [Authorize(Roles = $"{Consts.RoleConsts.Admin}")]
        public async Task<IActionResult> Add()
        {
            var roles = await userService.GetAllRolesAsync();
            return View(new UserAddDTO { Roles = roles });
        }



        [HttpPost]
        [Authorize(Roles = $"{Consts.RoleConsts.Admin}")]
        public async Task<IActionResult> Add(UserAddDTO userAddDTO)
        {
            var map = mapper.Map<AppUser>(userAddDTO);
            map.UserName = userAddDTO.Email;
            var roles = await userService.GetAllRolesAsync();

            var resultValidator = await validator.ValidateAsync(map);

            if (resultValidator.IsValid)
            {

                var result = await userService.CreateUser(userAddDTO, map);

                if (result.Succeeded)
                {

                    toastNotification.AddSuccessToastMessage(Messages.Category.Add(userAddDTO.Email), new ToastrOptions() { Title = "Uğurlu" });
                    return RedirectToAction("Add", "User", new { Area = "Admin" });
                }


                else
                {
                    foreach (var errors in result.Errors)
                        ModelState.AddModelError("", errors.Description);
                    return View(new UserAddDTO { Roles = roles });
                }
            }


            else
            {
                resultValidator.AddToModelState(ModelState);
            }

            return View(new UserAddDTO { Roles = roles });
        }

        [HttpGet]
        [Authorize(Roles = $"{Consts.RoleConsts.Superadmin}")]
        public async Task<IActionResult> Update(string userId)
        {

            var user = await userService.FindUserAsync(userId);
            var map = mapper.Map<UserUpdateDTO>(user);
            var roles = await userService.GetAllRolesAsync();
            map.Roles = roles;
            return View(map);
        }

        [HttpPost]
        [Authorize(Roles = $"{Consts.RoleConsts.Admin}")]
        public async Task<IActionResult> Update(UserUpdateDTO userUpdateDTO)
        {


            var map = mapper.Map<AppUser>(userUpdateDTO);
            var validResult = await validator.ValidateAsync(map);
            var roles = await userService.GetAllRolesAsync();

            if (validResult.IsValid)
            {
                var result = await userService.UpdateUser(userUpdateDTO);

                if (ModelState.IsValid)
                {

                    if (result.Succeeded)
                    {

                        toastNotification.AddSuccessToastMessage(Messages.User.Update(userUpdateDTO.Email), new ToastrOptions() { Title = "Uğurlu" });
                        return RedirectToAction("Index", "User", new { Area = "Admin" });
                    }

                    else
                    {
                        foreach (var errors in result.Errors)
                            ModelState.AddModelError("", errors.Description);
                        return View(new UserAddDTO { Roles = roles });
                    }

                }


            }

            else
            {
                validResult.AddToModelState(ModelState);
                return View(new UserUpdateDTO { Roles = roles });
            }



            return NotFound();

        }

        [HttpGet]
        [Authorize(Roles = $"{Consts.RoleConsts.Superadmin},{Consts.RoleConsts.Admin}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var result = await userService.DeleteUserAsync(userId.ToString());
            if (result.Item1.Succeeded)
            {
                toastNotification.AddSuccessToastMessage(Messages.User.Delete(result.Item2), new ToastrOptions() { Title = "Uğurlu" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });
            }


            else
            {
                var roles = await userService.GetAllRolesAsync();
                foreach (var errors in result.Item1.Errors)
                    ModelState.AddModelError("", errors.Description);
            }
            return RedirectToAction("Index", "User", new { Area = "Admin" });

        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var result = await userService.GetUserProfileAsync();
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDTO userProfileDto)
        {

            if (ModelState.IsValid)
            {
                var result = await userService.UserProfileUpdateAsync(userProfileDto);
                if (result)
                {
                    toastNotification.AddSuccessToastMessage("Profil yenilənmə işi tamamlandı", new ToastrOptions { Title = "İşlem Başarılı" });
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                else
                {
                    var profile = await userService.GetUserProfileAsync();
                    toastNotification.AddErrorToastMessage("Profil yenilənmə işi tamamlanmadı", new ToastrOptions { Title = "İşlem Başarısız" });
                    return View(profile);
                }
            }
            else
                return NotFound();
        }



    }
}

