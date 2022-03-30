using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ErrorLog
    {
        [Key]
        public int  Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Token { get; set; }
        public string Method { get; set; }
        public DateTime DateTime { get; set; }
    }
}
