using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;
using Academy.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Academy.Pages.Admin.StudentGroups
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        private IStudentGroup _studentGroup;

        public IndexModel(IStudentGroup studentGroup)
        {
            _studentGroup = studentGroup;
        }

        public List<StudentGroup> StudentGroups { get; set; }

        [BindProperty]
        public StudentGroup StudentGroup { get; set; }

        public async Task<IActionResult> OnGet(int id = 0)
        {
            StudentGroup = id != 0 ? await _studentGroup.GetById(id) : new StudentGroup();
            StudentGroups = await _studentGroup.GetAll();
            return Page();

        }
        public async Task<IActionResult> OnGetDelete(int id)
        {
            await _studentGroup.Delete(id);
            return Redirect("/Admin/StudentGroups");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("StudentGroup.SubjectClasses"); 
            if (ModelState.IsValid)
            {
                await _studentGroup.Upsert(StudentGroup);
            } 
            return Redirect("/Admin/StudentGroups");
        }
    }
}
