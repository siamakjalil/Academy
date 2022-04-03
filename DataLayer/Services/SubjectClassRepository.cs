using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public class SubjectClassRepository : ISubjectClass
    {
        private AcademyDbContext _db;

        public SubjectClassRepository(AcademyDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<SubjectClass>> GetAll(Expression<Func<SubjectClass, bool>> where = null, int skip = 0, int take = 0)
        {
            if (where == null)
            {
                return await _db.SubjectClasses.Include(u=>u.StudentGroup).Include(u=>u.Subject).Include(u=>u.Teacher)
                    .OrderByDescending(u => u.DateTime).ToListAsync();
            }
            else
            {
                return await _db.SubjectClasses.Where(where).Include(u => u.StudentGroup).Include(u => u.Subject)
                    .Include(u => u.Teacher).OrderByDescending(u => u.DateTime).ToListAsync();
            }
        }

        public async Task<int> GetCount(Expression<Func<SubjectClass, bool>> where = null)
        {
            if (where == null)
            {
                return await _db.SubjectClasses.CountAsync();
            }
            else
            {
                return await _db.SubjectClasses.Where(where).CountAsync();
            }
        }

        public async Task<SubjectClass> GetById(int id)
        {
            return await _db.SubjectClasses.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Upsert(SubjectClass appClass)
        {
            if (appClass.Id == 0)
            {
                appClass.DateTime = DateTime.Now;
                await _db.SubjectClasses.AddAsync(appClass);
            }
            else
            {
                _db.SubjectClasses.Update(appClass);
            }

            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var model = await GetById(id);
            if (model != null)
            {
                _db.SubjectClasses.Remove(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
