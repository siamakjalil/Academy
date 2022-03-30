using DataLayer.Interfaces;
using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Academy.Pages.Admin.Planing
{
    public class IndexModel : PageModel
    {
        IPlan _plan;
        ISubjectClass _subjectClass;
        public IndexModel(IPlan plan, ISubjectClass subjectClass)
        {
            _plan = plan;
            _subjectClass = subjectClass;
        }

        [BindProperty]
        public Plan Plan { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "لطفا کلاس ها را مشخص کنید")]
        public List<int> SubjectClassId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ViewData["SubjectClassId"] = new SelectList(await _subjectClass.GetAll(), "Id", "Title");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Plan.PlanDetails");
            if (ModelState.IsValid)
            {
                await _plan.Add(Plan);
                return Redirect("/Admin/Planing");
            }
            return Page();
        }
    }
}
