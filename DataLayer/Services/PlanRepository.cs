using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class PlanRepository : IPlan
    {
        private AcademyDbContext _db;

        public PlanRepository(AcademyDbContext db)
        {
            _db = db;
        }

        public async Task Add(Plan plan)
        {
            await _db.AddAsync(plan);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
