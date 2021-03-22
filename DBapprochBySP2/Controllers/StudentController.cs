using DBapprochBySP2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBapprochBySP2.Controllers
{
    public class StudentController : Controller
    {
        private StudentBySPEntities db = new StudentBySPEntities();
        // GET: Student
        public ActionResult Index(int Cpage=1)
        {
            ViewBag.PageCount = (int)Math.Ceiling((double)((decimal)db.Students.Count() / 10));
            ViewBag.CPage = Cpage;
           var s =  db.Database.SqlQuery<SelectStudents_Result>(
               "exec sp_pagination @PageNumber,@PageSize",
               new SqlParameter("@PageNumber", Cpage),new SqlParameter("@PageSize",10)).ToList();
            return View(s);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            db.InsertStudent(student.StudentId, student.Name, student.RollNo, student.Standard, student.City);
            /*return View();*/
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            var s = db.FindStudent(id).FirstOrDefault();
            return View(s);
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            db.UpdateStudent(student.StudentId, student.Name, student.RollNo, student.Standard, student.City);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            db.DelteStudent(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var s = db.FindStudent(id).FirstOrDefault();
            return View(s);
        }
    }
}