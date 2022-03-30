using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IPreSubject : IDisposable
    {
        Task<List<PreSubjectsViewModel>> GetAll(Expression<Func<PreSubjectsViewModel, bool>> @where = null);
        Task Add(PreSubject teacher);
        Task Delete(int id);
    }
}
