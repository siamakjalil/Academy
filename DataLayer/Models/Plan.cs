using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/mm/dd}")]
        public DateTime StartDate { get; set; } 
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/mm/dd}")]
        public DateTime EndDate { get; set; } 
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int NumberOfClass { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int RestTime { get; set; }
        public Plan()
        {

        }
        public virtual List<PlanDetail> PlanDetails { get; set; }
    }
}
