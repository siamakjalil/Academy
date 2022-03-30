using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Interfaces; 
using Helper; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; 

namespace Academy.Pages.Account
{
    [Authorize]
    public class ChangePassModel : PageModel
    { 
        private IAdmin _admin;

        public ChangePassModel(IAdmin admin)
        {
            _admin = admin;
        }

        public class InputModel
        {
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور فعلی")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string OldPass { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [StringLength(100, ErrorMessage = "حداقل 6 کاراکتر وارد کنید", MinimumLength = 6)]
            public string Pass { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "تکرار کلمه عبور")]
            [Compare("Pass", ErrorMessage = "کلمه عبور و تکرار آن یکی نیستند.")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string RePass { get; set; }
        }
        [BindProperty]
        public InputModel CngPass { get; set; }

        public string SuccessMsg { get; set; }

        public async Task<IActionResult> OnGet()
        {   
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userName = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value;
                var admin =await _admin.FirstOrDefault(u => u.UserName == userName);
                var pass = CngPass.OldPass.EncodePassword(admin.PassKey);
                if (pass != admin.Password)
                {
                    ModelState.AddModelError("CngPass.OldPass", "کلمه عبور فعلی اشتباه است.");
                    return Page();
                }

                if (CngPass.Pass!=CngPass.RePass)
                {
                    ModelState.AddModelError("CngPass.Pass", "کلمه عبور و تکرار آن یکی نیستند.");
                    return Page();
                }

                admin.PassKey = 10.GeneratePassword();
                admin.Password = CngPass.Pass.EncodePassword(admin.PassKey);
                await _admin.Upsert(admin);
                SuccessMsg = "کلمه عبور با موفقیت تغییر کرد.";
                return Page();
            }

            ModelState.AddModelError("CngPass.OldPass", "خطا در ثبت اطلاعات.");
            return Page();
        }
    }
}
