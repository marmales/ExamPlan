using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;
namespace UniversitySite.Models
{
    public class GradesListView
    {
        
        private IEnumerable<Grade> _studentGrades;
        public int StudentID { get; }
        public IEnumerable<Grade> StudentGrades { get { return _studentGrades; } set { _studentGrades = value; } }

        public GradesListView(IEnumerable<Grade> Repository, int ID)
        {
            StudentID = ID;
            _studentGrades = Repository.Where(x => x.StudentID == StudentID);
        }

    }
}