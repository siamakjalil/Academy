using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class TeacherSubject
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public TeacherSubject()
        {

        }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

    }
}
