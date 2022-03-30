using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class PlanViewModel
    {

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public DateTime StartDate { get; set; } 
        public string StartDateStr { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public DateTime EndDate { get; set; } 
        public string EndDateStr { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int StartTime { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int EndTime { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int NumberOfClass { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int RestTime { get; set; }
        
    }
}
