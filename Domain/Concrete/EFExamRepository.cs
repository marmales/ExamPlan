using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFExamRepository : IExamRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Exam> Exams { get { return context.Exams; } }
    }
}
