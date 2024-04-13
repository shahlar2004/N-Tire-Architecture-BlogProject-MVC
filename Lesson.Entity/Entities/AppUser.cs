using Lesson.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.Entities
{
    public class AppUser:IdentityUser<Guid>,IEntityBase
    {

        public AppUser()
        {
            ImageId = Guid.Parse("53C70E42-4494-47E0-8391-43AED02DADD3");
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? ImageId { get; set; }
        public Image Image { get; set; } 
        public ICollection<Article> Articles { get; set; }

    }
}
