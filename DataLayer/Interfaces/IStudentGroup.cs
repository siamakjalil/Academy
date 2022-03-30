using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IStudentGroup : IDisposable
    {
        Task<List<StudentGroup>> GetAll(Expression<Func<StudentGroup, bool>> @where = null,int skip=0,int take=10);
        Task<int> GetCount(Expression<Func<StudentGroup, bool>> @where = null);
        Task<StudentGroup> GetById(int id);
        Task Upsert(StudentGroup studentGroup);
        Task Delete(int id);
    }
}
