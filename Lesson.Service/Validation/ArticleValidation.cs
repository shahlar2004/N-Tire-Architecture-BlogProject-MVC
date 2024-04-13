using FluentValidation;
using Lesson.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Service.Validation
{
    public class ArticleValidation: AbstractValidator<Article>
    {
        public ArticleValidation()
        {
             RuleFor(x => x.Tutle)
            .NotEmpty().
            NotNull().
            MinimumLength(3).
            MaximumLength(150).
            WithName("Başlıq");


           RuleFor(x => x.Content)
          .NotEmpty().
          NotNull().
          MinimumLength(3).
          MaximumLength(1500).
          WithName("Məzmun");
        }
    }
}
