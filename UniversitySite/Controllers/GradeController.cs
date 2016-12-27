using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using UniversitySite.Models;

namespace UniversitySite.Controllers
{
    public class GradeController : Controller
    {
        private IGradeRepository GradeRepo;
        private int PageSize = 8;

        public GradeController(IGradeRepository repo)
        {
            GradeRepo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListGrades(int StudentID)
        {
            GradesListView modelView = new GradesListView(GradeRepo.Grades, StudentID);
            ViewBag.StudentName = modelView.StudentGrades.FirstOrDefault().Student.FirstName + " " + modelView.StudentGrades.FirstOrDefault().Student.LastName;
            return View(modelView);
        }


        public ActionResult Edit(int ID)
        {
            return View(GradeRepo.Grades.SingleOrDefault(x => x.GradeID == ID));
        }
        
        [HttpPost]
        public ActionResult Change(int ID, int mark)
        {
            GradeRepo.UpdateRecord(ID, mark);

            return RedirectToAction("ListGrades", new { StudentID = GradeRepo.Grades.SingleOrDefault(x => x.GradeID == ID).StudentID });
        }
 


    }
}
