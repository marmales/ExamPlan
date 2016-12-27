using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFGradeRepository : IGradeRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IEnumerable<Grade> Grades { get { return _context.Grades; } }
        public void UpdateRecord (int ID, int mark)
        {
            var original = _context.Grades.Find(ID);
            if(original != null)
            {
                original.Grade1 = mark;
                original.EntryDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
