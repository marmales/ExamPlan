using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
namespace UniversitySite.Controllers
{
    public class TablesController : Controller
    {
        //I know i shouldn't do this but i wanted to easly show tables in database
        private EFDbContext _context;

        public TablesController()
        {
            _context = new EFDbContext();
        }

        public ActionResult ER()
        {
            return View();
        }

        public ActionResult Students()
        {
            return View(_context.Students.ToList());
        }

        public ActionResult Subjects()
        {
            return View(_context.Subjects.ToList());
        }

        public ActionResult Lecturels()
        {
            return View(_context.Lecturels.ToList());
        }

        public ActionResult Classes()
        {
            return View(_context.Classes.ToList());
        }
    }
}