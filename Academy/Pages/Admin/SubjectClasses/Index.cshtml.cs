using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Models;
using Academy.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Academy.Classes;

namespace Academy.Pages.Admin.SubjectClasses
{
    [ClaimRequirement]
    public class IndexModel : PageModel
    {
        private ISubjectClass _subjectClass;

        public IndexModel(ISubjectClass subjectClass)
        {
            _subjectClass = subjectClass;
        }

        public PagingTagHelper.PagingInfo PagingInfo { get; set; }
        public List<SubjectClass> SubjectClasses { get; set; }

        public async Task<IActionResult> OnGet(int pageId = 1)
        {
            int take = int.MaxValue;
            SubjectClasses = await Filter(pageId, take);
            await PagingModel(pageId);
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            await _subjectClass.Delete(id);

            return Redirect("/Admin/SubjectClasses");
        }
        #region --utility--

        private async Task<List<SubjectClass>> Filter(int pageId, int take)
        {
            return await _subjectClass.GetAll(null, Utility.SkipNo(pageId, take), take);
        }


        private async Task PagingModel(int pageId)
        {
            PagingInfo = new PagingTagHelper.PagingInfo()
            {
                CurrentPage = pageId,
                ItemPerPage = int.MaxValue,
                TotalItems = await _subjectClass.GetCount()
            };
        }
        #endregion
    }
}
