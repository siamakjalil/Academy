using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;
using Helper;
using Academy.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Academy.Pages.Admin.SubjectClasses
{
    [ClaimRequirement]
    public class UpsertModel : PageModel
    {
        private ISubjectClass _appClass;
        private IStudentGroup _studentGroup;
        private ISubject _subject;
        ITeacherSubjects _teacherSubjects;

        public UpsertModel(ISubjectClass appClass, IStudentGroup studentGroup, ISubject subject, ITeacherSubjects teacherSubjects)
        {
            _appClass = appClass;
            _studentGroup = studentGroup;
            _subject = subject;
            _teacherSubjects = teacherSubjects;
        }

        [BindProperty]
        public SubjectClass SubjectClass { get; set; }
        [BindProperty]
        public int teacherId { get; set; }
        [BindProperty]
        public int TimePerClass { get; set; }
        [BindProperty]
        public int TimePerWeek { get; set; }

        public async Task<IActionResult> OnGet(int id = 0)
        {
            await SetData(id);
            return Page();
        }

        private async Task SetData(int id)
        {
            ViewData["StudentGroupId"] = new SelectList(await _studentGroup.GetAll(), "Id", "Title");
            ViewData["SubjectId"] = new SelectList(await _subject.GetAll(), "Id", "Title");
            SubjectClass = id != 0 ? await _appClass.GetById(id) : new SubjectClass();
            if (id != 0)
            { 
                var model = await _teacherSubjects.GetAll(u => u.SubjectId == SubjectClass.SubjectId);
                ViewData["TeacherId"] = new SelectList(model.Select(u => u.Teacher), "Id", "FullName", SubjectClass.TeacherId);

                TimePerClass = SubjectClass.TimePerClass.Hours;
                TimePerWeek = SubjectClass.TimePerWeek.Hours;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                if(TimePerClass > 23 || TimePerWeek > 23)
                {

                    ModelState.AddModelError("SubjectClass.Id", "بیشترین مقدار مجاز تعداد ساعات 23 است.");
                }

                ModelState.Remove("SubjectClass.StudentGroup");
                ModelState.Remove("SubjectClass.Subject");
                ModelState.Remove("SubjectClass.DateTime");
                ModelState.Remove("SubjectClass.Teacher");
                ModelState.Remove("SubjectClass.TeacherId");
                ModelState.Remove("SubjectClass.PlanDetails");
                ModelState.Remove("SubjectClass.TimePerWeek");
                ModelState.Remove("SubjectClass.TimePerClass");

                var flag = true;
                if (SubjectClass.Id == 0 && teacherId == 0 )
                {
                    flag = false;
                }

                if (ModelState.IsValid && flag)
                {
                    SubjectClass.TeacherId = teacherId;

                    SubjectClass.TimePerClass = new TimeSpan(TimePerClass, 0, 0);
                    SubjectClass.TimePerWeek = new TimeSpan(TimePerWeek, 0, 0);

                    await _appClass.Upsert(SubjectClass);
                    return Redirect("/Admin/SubjectClasses");
                }

                ModelState.AddModelError("SubjectClass.Id", "اطلاعات را به طور کامل وارد کنید.");
            }
            catch (Exception)
            {
                ModelState.AddModelError("SubjectClass.Id", "خطا در ثبت اطلاعات");
            }
            await SetData(SubjectClass.Id);
            return Page();

        }
    }
}
