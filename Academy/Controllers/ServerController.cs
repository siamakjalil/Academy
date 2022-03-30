using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace Academy.Controllers
{
    public class ServerController : Controller
    {
        ITeacherSubjects _teacherSubjects;
        public ServerController(ITeacherSubjects teacherSubjects)
        {
            _teacherSubjects = teacherSubjects;
        }
        public async Task<IActionResult> GetTeachers(int subjectId)
        {
            var model =await _teacherSubjects.GetAll(u => u.SubjectId == subjectId);
            ViewData["TeacherId"] = new SelectList(model.Select(u=>u.Teacher), "Id", "FullName");
            return PartialView("/Pages/Admin/Teachers/_TeacherList.cshtml", model);
        }
    }
}
