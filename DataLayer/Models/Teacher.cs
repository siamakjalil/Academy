using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string LastName { get; set; } 
        public string FullName => $"{FirstName} {LastName}";
        public Teacher()
        {

        }
        public virtual List<TeacherSubject> TeacherSubjects { get; set; }
        public virtual List<SubjectClass> SubjectClasses { get; set; }
    }
}
