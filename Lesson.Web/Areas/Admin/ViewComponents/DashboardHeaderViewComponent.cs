using AutoMapper;
using Lesson.Data.UnitOfWorks;
using Lesson.Entity.DTOs.Users;
using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Web.Areas.Admin.ViewComponents
{
    public class DashboardHeaderViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DashboardHeaderViewComponent(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser =  await userManager.GetUserAsync(HttpContext.User);
            var map= mapper.Map<UserDTO>(loggedInUser);
            var role =  string.Join("", await userManager.GetRolesAsync(loggedInUser));
            var image= await unitOfWork.GetRepository<Image>().GetByGuidAsync(loggedInUser.ImageId); 
            loggedInUser.Image = image;
            map.Role = role;
            map.Image = image;  
            return View(map);

        }
    }
}
