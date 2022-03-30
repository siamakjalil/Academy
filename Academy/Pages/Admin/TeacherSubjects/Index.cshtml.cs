using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;
using Academy.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Academy.Pages.Admin.TeacherSubjects
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        private ITeacherSubjects _teacherSubject;
        private ISubject _subject;

        public IndexModel(ITeacherSubjects teacherSubject, ISubject subject)
        {
            _teacherSubject = teacherSubject;
            _subject = subject;
        }

        public List<TeacherSubject> TeacherSubjects { get; set; }

        [BindProperty]
        public TeacherSubject TeacherSubject { get; set; }

        public async Task<IActionResult> OnGet(int id = 0)
        {
            if (id == 0)
            {
                return Redirect("/Admin/Teachers");
            }

            TeacherSubjects = await _teacherSubject.GetAll(u => u.TeacherId == id);
            TeacherSubject = new TeacherSubject()
            {
                TeacherId = id
            };
            var hasSubjects = TeacherSubjects.Select(u => u.SubjectId).ToList();
            var subjects = await _subject.GetAll(u => !hasSubjects.Contains(u.Id));
            ViewData["SubjectId"] = subjects.Any() ? new SelectList(subjects, "Id", "Title") : null;

            return Page();

        }
        public async Task<IActionResult> OnGetDelete(int id , int teacherId)
        {
            await _teacherSubject.Delete(id);
            return Redirect("/Admin/TeacherSubjects?id=" + teacherId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("TeacherSubject.Teacher");
            ModelState.Remove("TeacherSubject.Subject");
            if (ModelState.IsValid)
            {
                await _teacherSubject.Add(TeacherSubject);
            }
            return Redirect("/Admin/TeacherSubjects?id=" + TeacherSubject.TeacherId);
        }
    }
}
