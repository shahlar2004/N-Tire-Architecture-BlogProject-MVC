﻿using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.DTOs.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }    

        public string LastName { get; set; }

        public string Email { get; set; }   
        public bool EmailConfirmed { get; set; }   

        public string PhoneNumber { get; set; } 

        public int AccessFailedCount {  get; set; } 

        public Guid ImageId { get; set; }   
        public Image Image { get; set; }
        public string Role {  get; set; }   

    }

}
