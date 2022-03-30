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
    public class TeacherSubjectsRepository : ITeacherSubjects
    {
        private AcademyDbContext _db;

        public TeacherSubjectsRepository(AcademyDbContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<TeacherSubject>> GetAll(Expression<Func<TeacherSubject, bool>> where = null)
        {
            if (where == null)
            {
                return new List<TeacherSubject>();
            }
            else
            {
                return await _db.TeacherSubjects.Include(u=>u.Teacher).Include(u=>u.Subject).Where(where).ToListAsync();
            }
        }

        public async Task Add(TeacherSubject teacher)
        {
            await _db.TeacherSubjects.AddAsync(teacher);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await _db.TeacherSubjects.FindAsync(id);
            if (model != null)
            {
                _db.TeacherSubjects.Remove(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
