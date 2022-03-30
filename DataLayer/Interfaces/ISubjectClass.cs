using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface ISubjectClass:IDisposable
    {
        Task<List<SubjectClass>> GetAll(Expression<Func<SubjectClass, bool>> where = null,int skip=0,int take=10);
        Task<int> GetCount(Expression<Func<SubjectClass, bool>> where = null);
        Task<SubjectClass> GetById(int id);
        Task Upsert(SubjectClass appClass);
        Task Delete(int id);
    }
}
