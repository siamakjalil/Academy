using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ITeacher:IDisposable
    {
        Task<List<Teacher>> GetAll(Expression<Func<Teacher, bool>> @where = null,int skip=0,int take=10);
        Task<int> GetCount(Expression<Func<Teacher, bool>> @where = null);
        Task<Teacher> GetById(int id);
        Task Upsert(Teacher teacher);
        Task Delete(int id);
    }
}
