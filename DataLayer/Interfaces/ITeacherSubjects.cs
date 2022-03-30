using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ITeacherSubjects : IDisposable
    {
        Task<List<TeacherSubject>> GetAll(Expression<Func<TeacherSubject, bool>> @where = null);
        Task Add(TeacherSubject teacher);
        Task Delete(int id);
    }
}
