using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class PreSubjectsViewModel
    {
        public int Id { get; set; }
        public int PreId { get; set; }
        public int SubjectId { get; set; }
        public string Pre { get; set; }
        public string Subject { get; set; }
    }
}
