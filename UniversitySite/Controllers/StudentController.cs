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
    public class StudentController : Controller
    {
        
        private IStudentsRepository StudentRepo;
        private int PageSize = 8;
        public StudentController(IStudentsRepository studentRepo)
        {
            StudentRepo = studentRepo;
        }
       
        public ActionResult List(int page = 1)
        {
            StudentsListView modelView = new StudentsListView
            {
                Students = StudentRepo.Students
                    .OrderBy(s => s.StudentID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = StudentRepo.Students.Count()
                }
            };

            return View(modelView);

        }


    }
}