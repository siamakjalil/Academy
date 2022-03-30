using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using Academy.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Academy.Pages.Admin
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        ISubject _subject;
        IStudentGroup _studentGroup;
        ISubjectClass _class;
        ITeacher _teacher;
        public IndexModel(ISubject subject, IStudentGroup studentGroup, ISubjectClass subjectClass, ITeacher teacher)
        {
            _subject = subject;
            _studentGroup = studentGroup;
            _class = subjectClass;
            _teacher = teacher;
        }
        public async Task<IActionResult> OnGet()
        {
            ViewData["Group"] =await _studentGroup.GetCount();
            ViewData["Subject"] = await _subject.GetCount();
            ViewData["Teacher"] = await _teacher.GetCount();
            ViewData["Class"] = await _class.GetCount();
            return Page();
        }
    }
}
