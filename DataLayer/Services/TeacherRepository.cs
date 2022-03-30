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
    public class TeacherRepository : ITeacher
    {
        private AcademyDbContext _db;

        public TeacherRepository(AcademyDbContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<Teacher>> GetAll(Expression<Func<Teacher, bool>> where = null, int skip = 0, int take = 10)
        {
            if (where == null)
            {
                return await _db.Teachers.OrderByDescending(u => u.Id)
                    .Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _db.Teachers.Where(where)
                    .Skip(skip).Take(take).ToListAsync();
            }
        }
        public async Task<int> GetCount(Expression<Func<Teacher, bool>> where = null)
        {
            if (where == null)
            {
                return await _db.Teachers.CountAsync();
            }
            else
            {
                return await _db.Teachers.Where(where).CountAsync();
            }
        }

        public async Task<Teacher> GetById(int id)
        {
            return await _db.Teachers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Upsert(Teacher teacher)
        {
            if (teacher.Id == 0)
            { 
                await _db.Teachers.AddAsync(teacher);
            }
            else
            {
                _db.Teachers.Update(teacher);
            }

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await GetById(id);
            if (model!=null)
            {
                _db.Teachers.Remove(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
