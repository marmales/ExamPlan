using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Concrete
{
    public class EFStudentRepository : IStudentsRepository
    {
        private EFDbContext context = new EFDbContext();
       
        public IEnumerable<Student> Students { get { return context.Students; } }
        
    }
}
