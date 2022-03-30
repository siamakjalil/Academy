using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IAdmin : IDisposable
    {
        Task<List<Admin>> GetAll(Expression<Func<Admin, bool>> where = null,int skip = 0 , int take=10);
        Task<int> GetCount(Expression<Func<Admin, bool>> where = null);

        Task<Admin> FirstOrDefault(Expression<Func<Admin, bool>> where = null);
        Task Upsert(Admin users); 
    }
}
