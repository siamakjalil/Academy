using DataLayer.Interfaces;
using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Helper;

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

        [BindProperty]
        [Required(ErrorMessage = "لطفا تاریخ شروع را مشخص کنید")]
        public string StartDate{ get; set; }
        [BindProperty]
        [Required(ErrorMessage = "لطفا تاریخ پایان را مشخص کنید")]
        public string EndDate{ get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["SubjectClassId"] = new SelectList((await _subjectClass.GetAll()).OrderBy(u=>u.Title), "Id", "Title");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Plan.PlanDetails");
            ModelState.Remove("Plan.StartDate");
            ModelState.Remove("Plan.EndDate");
            if (ModelState.IsValid)
            {
                Plan.StartDate = StartDate.ShamsiToMiladi()??DateTime.Now;
                Plan.EndDate = EndDate.ShamsiToMiladi()??DateTime.Now;
                await _plan.Add(Plan);
                return Redirect("/Admin/Planing");
            }
            return Page();
        }
    }
}
