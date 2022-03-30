using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public Subject()
        {

        }
        public virtual List<TeacherSubject> TeacherSubjects { get; set; }
        //public virtual List<PreSubject> PreSubjects { get; set; }
        public virtual List<SubjectClass> SubjectClasses { get; set; }
    }
}
