using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
   public class AcademyDbContext:DbContext
    {
        public AcademyDbContext(DbContextOptions<AcademyDbContext> option):base(option)
        {
            
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<PreSubject> PreSubjects { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectClass> SubjectClasses { get; set; }    
        public DbSet<Teacher> Teachers { get; set; }    
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }    


    }
}
