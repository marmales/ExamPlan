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
        private Graph _examList;
        private IList<Vertex> _vertexes = new List<Vertex>();

        public IEnumerable<Exam> Exams
        {
            get { return _examList.ColorOurGraph().Select(x => (Exam)x); }
        }
        
        public void SaveFile(string path)
        {
            _examList.ColorOurGraph();
            _examList.GetJSdata(path);
        }
        //public string ExamsDate { get { _examList.CreateMatrix(); return _examList.GetJSdata("exams"); } }

        public ExamsListView(IEnumerable<Student> students, IEnumerable<Exam> exams, DateTime SetFirstExamDate)
        {
            foreach (Exam item in exams)
            {
                _vertexes.Add(new Vertex(students.Where(x => x.StudentsLists.Any(y => y.Class.SubjectID == item.SubjectID)), item));
            }

            _examList = new Graph(_vertexes, SetFirstExamDate);
        }
    }
}