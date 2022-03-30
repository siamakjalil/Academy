using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; } 

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        public string PassKey { get; set; }   

        public Admin()
        {

        } 
    }
}
