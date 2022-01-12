using School.DataAccess;
using School.DataAccess.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace School.MvcUi.Controllers
{
    public class StudentGradeController : Controller
    {
        private SchoolDb db = new SchoolDb();

        // GET: StudenGrade
        public ActionResult Index()
        {
            var studentGrades = db.StudentGrades.Include(s => s.Course).Include(s => s.Person);
            return View(studentGrades.ToList());
        }

        // GET: StudenGrade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studenGrade = db.StudentGrades.Find(id);
            if (studenGrade == null)
            {
                return HttpNotFound();
            }
            return View(studenGrade);
        }

        // GET: StudenGrade/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.People, "PersonID", "LastName");
            return View();
        }

        // POST: StudenGrade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] StudentGrade studenGrade)
        {
            if (ModelState.IsValid)
            {
                db.StudentGrades.Add(studenGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studenGrade.CourseID);
            ViewBag.StudentID = new SelectList(db.People, "PersonID", "LastName", studenGrade.StudentID);
            return View(studenGrade);
        }

        // GET: StudenGrade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studenGrade = db.StudentGrades.Find(id);
            if (studenGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studenGrade.CourseID);
            ViewBag.StudentID = new SelectList(db.People, "PersonID", "LastName", studenGrade.StudentID);
            return View(studenGrade);
        }

        // POST: StudenGrade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] StudentGrade studenGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studenGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", studenGrade.CourseID);
            ViewBag.StudentID = new SelectList(db.People, "PersonID", "LastName", studenGrade.StudentID);
            return View(studenGrade);
        }

        public ActionResult Update(string id)
        {
            var studentGrades = id == null ? db.StudentGrades.Include(s => s.Course).Include(s => s.Person)
                : db.StudentGrades.Where(sg => sg.Course.Title == id)
                    .Include(s => s.Course)
                    .Include(s => s.Person);

            return View(studentGrades.ToList());
        }

        // POST: StudenGrade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(IEnumerable<StudentGrade> studenGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studenGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studenGrade);
        }

        // GET: StudenGrade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrade studenGrade = db.StudentGrades.Find(id);
            if (studenGrade == null)
            {
                return HttpNotFound();
            }
            return View(studenGrade);
        }

        // POST: StudenGrade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGrade studenGrade = db.StudentGrades.Find(id);
            db.StudentGrades.Remove(studenGrade);
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
