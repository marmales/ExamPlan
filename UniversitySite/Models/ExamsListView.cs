using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.ExamsCalculator;
using Domain.Abstract;
using Domain.Entities;
namespace UniversitySite.Models
{
    public class ExamsListView
    {
        private ExaminationSession _examList;
        private IList<ExamVertex> _vertexes = new List<ExamVertex>();
        private IEnumerable<Student> _students;


        public IEnumerable<Exam> getGeneratedExams
        {
            get { return _examList.ColorOurGraph().Select(x => (Exam)x); }
        }
        
        public ExamsListView(IEnumerable<Student> students, IEnumerable<Exam> exams, DateTime SetFirstExamDate)
        {
            _students = students;
            foreach (Exam item in exams)
            {
                _vertexes.Add(new ExamVertex( getStudentsByExam(item), item));
            }

            _examList = new ExaminationSession(_vertexes, SetFirstExamDate);
        }

        public void SaveFile(string path)
        {
            _examList.ColorOurGraph();
            _examList.CreateJSdata(path);
        }

        private IEnumerable<Student> getStudentsByExam(Exam checkedExam)
        {
            return _students.Where(x => x.StudentsLists.Any(y => y.Class.SubjectID == checkedExam.SubjectID));
        }
    }
}