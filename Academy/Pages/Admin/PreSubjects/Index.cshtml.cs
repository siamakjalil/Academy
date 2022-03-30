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
using DataLayer.ViewModels;

namespace Academy.Pages.Admin.PreSubjects
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        private IPreSubject _preSubject;
        private ISubject _subject;

        public IndexModel(IPreSubject preSubject, ISubject subject)
        {
            _preSubject = preSubject;
            _subject = subject;
        }

        public List<PreSubjectsViewModel> PreSubjects { get; set; }

        [BindProperty]
        public PreSubject PreSubject { get; set; }

        public async Task<IActionResult> OnGet(int id = 0)
        {
            if (id == 0)
            {
                return Redirect("/Admin/Subjects");
            }

            PreSubjects = await _preSubject.GetAll(u => u.SubjectId == id);
            PreSubject = new PreSubject()
            {
                SubjectId = id
            };
            var hasSubjects = PreSubjects.Select(u => u.PreId).ToList();
            var subjects = await _subject.GetAll(u => !hasSubjects.Contains(u.Id) && u.Id != id);
            ViewData["PreId"] = subjects.Any() ? new SelectList(subjects, "Id", "Title") : null;

            return Page();

        }
        public async Task<IActionResult> OnGetDelete(int id , int subjectId)
        {
            await _preSubject.Delete(id);
            return Redirect("/Admin/PreSubjects?id=" + subjectId);
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            if (ModelState.IsValid)
            {
                await _preSubject.Add(PreSubject);
            }
            return Redirect("/Admin/PreSubjects?id=" + PreSubject.SubjectId);
        }
    }
}
