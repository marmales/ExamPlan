using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Domain.Abstract
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> Grades { get; }
        void UpdateRecord(int ID, int mark);
    }
}
