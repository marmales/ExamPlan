﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using UniversitySite.Models;
namespace UniversitySite.Controllers
{
    public class ExamsController : Controller
    {
        private IStudentsRepository StudentRepo;
        private IExamRepository ExamRepo;
        private ExamsListView modelView;

        public ExamsController(IStudentsRepository studentRepo, IExamRepository ExamRepo)
        {
            StudentRepo = studentRepo;
            this.ExamRepo = ExamRepo;
            
        }

        [HttpGet]
        public ActionResult CalculateExamDate()
        {
            return View();
        }

        public ActionResult Calculate()
        {
            if(modelView == null)
                modelView =  new ExamsListView(StudentRepo.Students, ExamRepo.Exams, new DateTime(2016, 06, 12, 8, 0, 0));
            
            return View(modelView);
        }
            

        public ActionResult Graph()
        {

            if (modelView == null)
                modelView = new ExamsListView(StudentRepo.Students, ExamRepo.Exams, new DateTime(2016, 06, 12, 8, 0, 0));

            modelView.SaveFile("~/Scripts/tables.js");

            return View();
        }
    }
}