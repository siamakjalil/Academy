using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class SubjectClass
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int StudentGroupId { get; set; }
        [Display(Name = "درس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int SubjectId { get; set; }
        [Display(Name = "استاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TeacherId { get; set; }
        [MaxLength(150)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime DateTime { get; set; }
        [Display(Name = "زمان ثابت هر کلاس")]
        public int TimePerClass { get; set; }
        [Display(Name = "چند ساعت در هفته")]
        public int TimePerWeek { get; set; } 
        public int PerWeekCount()
        {
            return TimePerWeek / (TimePerClass == 0 ? 1 : TimePerClass);
        }
        public SubjectClass()
        {

        }
        [ForeignKey("StudentGroupId")]
        public virtual StudentGroup StudentGroup { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public virtual List<PlanDetail> PlanDetails { get; set; }
    }
}
