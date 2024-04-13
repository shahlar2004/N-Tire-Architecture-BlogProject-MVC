using FluentValidation;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Validation
{
    public class CategoryValidation:AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(x =>x.Name)
            .NotEmpty().
             NotNull().
             MinimumLength(3).
             MaximumLength(50).
            WithName("Ad");   
        }
    }
}
