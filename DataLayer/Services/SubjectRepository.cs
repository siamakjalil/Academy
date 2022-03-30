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
    public class SubjectRepository : ISubject
    {
        private AcademyDbContext _db;

        public SubjectRepository(AcademyDbContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<Subject>> GetAll(Expression<Func<Subject, bool>> where = null, int skip = 0, int take = 10)
        {
            if (where == null)
            {
                return await _db.Subjects.OrderByDescending(u => u.Id)
                    .Skip(skip).Take(take).ToListAsync();
            }
            else
            {
                return await _db.Subjects.Where(where)
                    .Skip(skip).Take(take).ToListAsync();
            }
        }
        public async Task<int> GetCount(Expression<Func<Subject, bool>> where = null)
        {
            if (where == null)
            {
                return await _db.Subjects.CountAsync();
            }
            else
            {
                return await _db.Subjects.Where(where).CountAsync();
            }
        }

        public async Task<Subject> GetById(int id)
        {
            return await _db.Subjects.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Upsert(Subject subject)
        {
            if (subject.Id == 0)
            { 
                await _db.Subjects.AddAsync(subject);
            }
            else
            {
                _db.Subjects.Update(subject);
            }

            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var model = await GetById(id);
            if (model != null)
            {
                _db.Subjects.Remove(model);
                await _db.SaveChangesAsync();
            }
        }
    }
}
