using FluentValidation;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Validation
{
    public class UserValidation:AbstractValidator<AppUser>
    {
        public UserValidation()
        {
            RuleFor(x => x.FirstName)
           .NotEmpty().
            NotNull().
            MinimumLength(3).
            MaximumLength(50).
            WithName("Ad");


            RuleFor(x => x.LastName)
           .NotEmpty().
            NotNull().
            MinimumLength(3).
            MaximumLength(50).
            WithName("Soyad");

            RuleFor(x => x.UserName)
           .NotEmpty().
            NotNull().
            MinimumLength(3).
            MaximumLength(50).
            WithName("İstifadəçi adı");

            RuleFor(x => x.Email).
             NotNull().
             MinimumLength(3).
             MaximumLength(50).
             WithName("Email");

        }
    }
}
