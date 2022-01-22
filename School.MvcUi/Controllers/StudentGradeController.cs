using School.DataAccess;
using School.DataAccess.Helpers;
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

        public ActionResult Index()
        {
            var studentGrades = db.StudentGrades.Include(s => s.Course).Include(s => s.Person);
            return View(studentGrades.ToList());
        }

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

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses.OrderBy(c => c.Title), "CourseID", "Title");
            ViewBag.StudentID = new SelectList(db.People, "PersonID", "LastName").OrderBy(c => c.Text);
            return View();
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection form)
        {
            var gradesToUpdate = new List<IdValue<decimal>>();
            if (!decimal.TryParse(form[form.AllKeys[1]], out decimal grade)) return View("Error");

            for (int i = 2; i < form.Count; i += 2)
            {
                if(!form[form.AllKeys[i + 1]].StartsWith("true")) continue;

                if (!int.TryParse(form[form.AllKeys[i]], out int id)) return View("Error");

                gradesToUpdate.Add(new IdValue<decimal> { Id = id, Value = grade });
            }

            if (gradesToUpdate.Count > 0) using (var repo = new StudentGradeRepository()) repo.SetGrades(gradesToUpdate);

            return RedirectToAction(nameof(Update));
        }

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
