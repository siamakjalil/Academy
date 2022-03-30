using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;


namespace Academy.Pages.Admin.Teachers
{
    public class UpsertModel : PageModel
    {
        ITeacher _teacher;
        public UpsertModel(ITeacher teacher)
        {
            _teacher = teacher;
        }

        [BindProperty]
        public Teacher Teacher { get; set; }
        public string Msg { get; set; }

        public async Task<IActionResult> OnGet(int id = 0 )
        {
            if(id != 0)
            {
                Teacher =await _teacher.GetById(id);
            }
            else
            {
                Teacher = new Teacher();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Teacher.FirstName) && !string.IsNullOrEmpty(Teacher.LastName))
            {
                await _teacher.Upsert(Teacher);
                return Redirect("/Admin/Teachers");
            }
            ModelState.AddModelError("", "خطا در ثبت اطلاعات");
            return Page();
        }
    }
}
