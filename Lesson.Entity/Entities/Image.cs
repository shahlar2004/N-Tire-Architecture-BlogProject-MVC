using Lesson.Core.Entities;
using Lesson.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Entity.Entities
{
    public class Image:EntityBase
    {

        public Image()
        {
                    
        }

        public Image(string FileName, string FileType, string createdBy)
        {
            this.FileName = FileName;
            this.FileType = FileType;
            CreatedBy = createdBy;
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
  
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
