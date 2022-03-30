using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ISubject : IDisposable
    {
        Task<List<Subject>> GetAll(Expression<Func<Subject, bool>> @where = null,int skip=0,int take=10);
        Task<int> GetCount(Expression<Func<Subject, bool>> @where = null);
        Task<Subject> GetById(int id);
        Task Upsert(Subject subject);
        Task Delete(int id);
    }
}
