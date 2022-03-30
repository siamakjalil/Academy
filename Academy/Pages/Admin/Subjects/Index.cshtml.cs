using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;
using Academy.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Academy.Pages.Admin.Subjects
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        private ISubject _subject;

        public IndexModel(ISubject subject)
        {
            _subject = subject;
        }

        public List<Subject> Subjects { get; set; }

        [BindProperty]
        public Subject Subject { get; set; }

        public async Task<IActionResult> OnGet(int id = 0)
        {
            Subject = id != 0 ? await _subject.GetById(id) : new Subject();
            Subjects = await _subject.GetAll();
            return Page();

        }
        public async Task<IActionResult> OnGetDelete(int id)
        {
            await _subject.Delete(id);
            return Redirect("/Admin/Subjects");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Subject.SubjectClasses");
            ModelState.Remove("Subject.TeacherSubjects");
            if (ModelState.IsValid)
            {
                await _subject.Upsert(Subject);
            } 
            return Redirect("/Admin/Subjects");
        }
    }
}
