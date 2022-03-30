using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PreSubject
    {
        [Key]
        public int Id { get; set; }
        public int PreId { get; set; }
        public int? SubjectId { get; set; }
        public PreSubject()
        {

        }

        //[ForeignKey("PreId")]
        //public virtual Subject Pre { get; set; }
        //[ForeignKey("SubjectId")]
        //public virtual Subject Subject { get; set; }


    }
}
