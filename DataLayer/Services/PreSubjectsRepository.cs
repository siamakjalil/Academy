using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public class PreSubjectsRepository : IPreSubject
    {
        private AcademyDbContext _db;

        public PreSubjectsRepository(AcademyDbContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<PreSubjectsViewModel>> GetAll(Expression<Func<PreSubjectsViewModel, bool>> where = null)
        {
            if (where == null)
            {
                return new List<PreSubjectsViewModel>();
            }
            else
            {
                var model=  await (from ps in _db.PreSubjects
                              join sub in _db.Subjects on ps.SubjectId equals sub.Id
                              join pre in _db.Subjects on ps.PreId equals pre.Id
                              select new PreSubjectsViewModel()
                              {
                                  Id = ps.Id,
                                  PreId = ps.PreId,
                                  Pre = pre.Title,
                                  Subject = sub.Title,
                                  SubjectId = ps.SubjectId ?? 0
                              }).Where(where).ToListAsync();
                return model.DistinctBy(u => u.Id).ToList();
            }
        }

        public async Task Add(PreSubject teacher)
        {
            await _db.PreSubjects.AddAsync(teacher);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await _db.PreSubjects.FindAsync(id);
            if (model != null)
            {
                _db.PreSubjects.Remove(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
