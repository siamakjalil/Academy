using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class StudentGroup
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public StudentGroup()
        {

        }
        public virtual List<SubjectClass> SubjectClasses { get; set; }
    }
}
