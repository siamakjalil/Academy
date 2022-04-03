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
    public class AdminRepository : IAdmin
    {
        private AcademyDbContext _db;

        public AdminRepository(AcademyDbContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<Admin>> GetAll(Expression<Func<Admin, bool>> @where = null, int skip = 0, int take = 10)
        {
            if (where == null)
            {
                return await _db.Admins
                    .OrderByDescending(u => u.Id).ToListAsync();
            }
            return await _db.Admins
                .Where(where).OrderByDescending(u => u.Id).ToListAsync();
        }

        public async Task<int> GetCount(Expression<Func<Admin, bool>> @where = null)
        {
            if (where == null)
            {
                return await _db.Admins.CountAsync();
            }
            return await _db.Admins.Where(where).CountAsync();
        }

        public async Task<Admin> FirstOrDefault(Expression<Func<Admin, bool>> @where = null)
        {
            if (where == null)
            {
                return null;
            }
            return await _db.Admins.FirstOrDefaultAsync(where);
        }

        public async Task Upsert(Admin Admins)
        {
            if (Admins.Id != 0)
            {
                _db.Admins.Update(Admins);
                await _db.SaveChangesAsync();
            }
            else
            {
                await _db.Admins.AddAsync(Admins);
                await _db.SaveChangesAsync();
            }
        }
    }
}
