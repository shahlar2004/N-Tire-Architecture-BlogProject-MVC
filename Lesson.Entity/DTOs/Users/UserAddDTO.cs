﻿using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.DTOs.Users
{
    public class UserAddDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public Guid RoleId { get; set; }
        public List<AppRole> Roles { get; set; }
    }
}
