using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Academy.Classes;
using Academy.TagHelpers;

namespace Academy.Pages.Admin.Teachers
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        ITeacher _teacher;
        public IndexModel(ITeacher teacher)
        {
            _teacher = teacher;
        }

        public PagingTagHelper.PagingInfo PagingInfo { get; set; }
        public List<Teacher> TeachersList { get; set; }

        public async Task<IActionResult> OnGet(int pageId = 1)
        {
           await SetModel(pageId);
            return Page();
        }

        private async Task SetModel(int pageId = 1)
        {

            int take = int.MaxValue;
            TeachersList = await _teacher.GetAll(null, Utility.SkipNo(pageId, take), take);
            PagingInfo = new PagingTagHelper.PagingInfo()
            {
                CurrentPage = pageId,
                ItemPerPage = take,
                TotalItems = await _teacher.GetCount()
            };
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            await _teacher.Delete(id);
            await SetModel();
            return Page();
        }
    }
}
