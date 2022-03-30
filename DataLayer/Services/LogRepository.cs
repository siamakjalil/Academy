using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Models;

namespace DataLayer.Services
{
    public class LogRepository : ILog
    {
        private AcademyDbContext _db;

        public LogRepository(AcademyDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
        public async Task ErrorLog(string msg, string exp, string path)
        {
            try
            {
                await _db.ErrorLogs.AddAsync(new ErrorLog
                {
                    DateTime = DateTime.Now,
                    Message = msg,
                    StackTrace = exp,
                    Method = path,
                    Token = ""
                });
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
