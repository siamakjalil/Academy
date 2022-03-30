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
    public class StudentGroupRepository : IStudentGroup
    {
        private AcademyDbContext _db;

        public StudentGroupRepository(AcademyDbContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<StudentGroup>> GetAll(Expression<Func<StudentGroup, bool>> where = null, int skip = 0, int take = 10)
        {
            if (where == null)
            {
                return await _db.StudentGroups.OrderByDescending(u => u.Id)
                    .Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _db.StudentGroups.Where(where)
                    .Skip(skip).Take(take).ToListAsync();
            }
        }
        public async Task<int> GetCount(Expression<Func<StudentGroup, bool>> where = null)
        {
            if (where == null)
            {
                return await _db.StudentGroups.CountAsync();
            }
            else
            {
                return await _db.StudentGroups.Where(where).CountAsync();
            }
        }

        public async Task<StudentGroup> GetById(int id)
        {
            return await _db.StudentGroups.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Upsert(StudentGroup studentGroup)
        {
            if (studentGroup.Id == 0)
            { 
                await _db.StudentGroups.AddAsync(studentGroup);
            }
            else
            {
                _db.StudentGroups.Update(studentGroup);
            }

            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var model = await GetById(id);
            if (model != null)
            {
                _db.StudentGroups.Remove(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
