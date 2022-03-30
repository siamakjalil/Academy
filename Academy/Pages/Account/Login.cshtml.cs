using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;
using Helper;
using Academy.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Academy.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private IAdmin _admin;
        private ILog _log;

        public LoginModel(IAdmin admin, ILog log)
        {
            _admin = admin;
            _log = log;
        }

        [BindProperty]
        public DataLayer.Models.Admin Input { get; set; } 


        public async Task OnGetAsync(string returnUrl = null, int rcy = 0)
        { 
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ModelState.Remove("returnUrl");
            ModelState.Remove("Input.PassKey");

            if (ModelState.IsValid)
            {
                var admin = await _admin.FirstOrDefault(u => u.UserName.ToLower() == Input.UserName.ToLower());
                if (admin != null)
                {
                    var pass = Helper.Password.EncodePassword(Input.Password, admin.PassKey);
                    if (pass == admin.Password)
                    {
                        var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, admin.UserName)
                                };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            new AuthenticationProperties
                            {
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(604800),
                                IsPersistent = true,
                                AllowRefresh = true
                            });
                        return Redirect("/Admin");
                    }
                }
            }

            ModelState.AddModelError("Input.UserName", "اطلاعات نامعتبر");
            return Page();
        }
    }
}
