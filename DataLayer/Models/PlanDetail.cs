using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PlanDetail
    {

        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int SubjectClassId { get; set; }
        public int DayId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public PlanDetail()
        {

        }

        [ForeignKey("PlanId")]
        public virtual Plan Plan { get; set; }

        [ForeignKey("SubjectClassId")]
        public virtual SubjectClass SubjectClass { get; set; }
    }
}
