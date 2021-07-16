using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticalTest.Context;
using PracticalTest.Models;

namespace PracticalTest.Controllers
{
    public class ExamController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Exam
        public ActionResult Index()
        {
            var exams = db.Exams.Include(e => e.ClassRoom).Include(e => e.Faculty).Include(e => e.Subject);
            return View(exams.ToList());
        }

        // GET: Exam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exam/Create
        public ActionResult Create()
        {
            ViewBag.ClassRoomID = new SelectList(db.ClassRooms, "Id", "Name");
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name");
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartTime,Date,Duration,SubjectID,FacultyID,ClassRoomID")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exams.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassRoomID = new SelectList(db.ClassRooms, "Id", "Name", exam.ClassRoomID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name", exam.FacultyID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "Name", exam.SubjectID);
            return View(exam);
        }

        // GET: Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassRoomID = new SelectList(db.ClassRooms, "Id", "Name", exam.ClassRoomID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name", exam.FacultyID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "Name", exam.SubjectID);
            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime,Date,Duration,SubjectID,FacultyID,ClassRoomID")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassRoomID = new SelectList(db.ClassRooms, "Id", "Name", exam.ClassRoomID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name", exam.FacultyID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "Id", "Name", exam.SubjectID);
            return View(exam);
        }

        // GET: Exam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = db.Exams.Find(id);
            db.Exams.Remove(exam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
