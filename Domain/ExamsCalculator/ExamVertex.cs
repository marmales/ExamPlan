
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Abstract;
using Domain.Concrete;

namespace Domain.ExamsCalculator
{
    public class ExamVertex
    {
        private Exam _exam;
        private DateTime? _date;
        private IEnumerable<Student> StudentsList;

        public double LengthInMinutes { get { return _exam.Duration.TotalMinutes; } }
        public DateTime? StartHour
        {
            get
            {
                //return ExamRepo.Exams.Single(x => x.SubjectID == _subjectID).DateAndHour;
                return _date;
            }
            set
            {
                _date = value;
            }
        }
        public string ExamName
        {
            get
            {
                return _exam.Subject.SubjectName;
            }
        }
        public int Color { get; set; }

        public ExamVertex(IEnumerable<Student> students, Exam exam)
        { 
           StudentsList = students;
            _exam = exam;
        }

        public static bool FindConflicts(ExamVertex v1, ExamVertex v2)
        {
            foreach (Student item in v1.StudentsList)
            {
                if (v2.StudentsList.Contains(item))
                    return true;
            }
            return false;
        }

        public static explicit operator Exam(ExamVertex vertex)
        {
            Exam tmp = new Exam
            {
                DateAndHour = vertex.StartHour,
                Duration = TimeSpan.FromMinutes(vertex.LengthInMinutes),
                ExamID = vertex._exam.ExamID,
                SubjectID = vertex._exam.SubjectID
            };
            tmp.Subject = vertex._exam.Subject;
            return tmp;
        }
        
    }
}
