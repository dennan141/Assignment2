using Microsoft.AspNetCore.Mvc;
using CoursesMVC.Models;
using CoursesMVC.Data;
using System.ComponentModel.DataAnnotations;

namespace CoursesMVC.Controllers
{

    //This is the Controller of MVC, controlling the logic and acting as a interface between Model and View
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CoursesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Returns view with all courses
        public IActionResult Index()
        {
            List<Course> objCoursesList = _db.Courses.ToList();
            return View(objCoursesList);
        }


        // ----- CREATE -----
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            //DateResult becomes, negative if Start is after End, 0 if they are same day
            //and becomes positive if Start is before End. I.e dateResult should be positive
            int dateResult = course.EndDate.CompareTo(course.StartDate);
            //This triggers if StartDate is before OR same day as EndDate. i.e incorrect order.
            if (dateResult <= 0)
            {
                ModelState.AddModelError("StartDate", "Start date needs to be BEFORE end date");
                return View(course);
            }

            if (!ModelState.IsValid)
            {
                return View(course);
            }
            //This is success
            _db.Courses.Add(course);
            _db.SaveChanges();
            TempData["success"] = "Course " + course.Title + " was created.";
            return RedirectToAction("Index");

        }

        // ----- EDIT -----
        public IActionResult Edit(int? courseId)
        {
            Course? foundCourse = _db.Courses.FirstOrDefault(obj => obj.Id == courseId);
            if (courseId == null || foundCourse == null)
            {
                //Returns a 404 page if Id does not exists.
                return NotFound();
            }
            return View(foundCourse);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Update(course);
                _db.SaveChanges();
                TempData["success"] = "Course " + course.Title + " was edited.";
                return RedirectToAction("Index");
            }
            
            return View(course);
        }


        // ----- DELETE -----


        public IActionResult Delete(int? courseId)
        {
            Course? foundCourse = _db.Courses.FirstOrDefault(obj => obj.Id == courseId);
            if (courseId == null || foundCourse == null)
            {
                //Returns a 404 page if Id does not exists.
                return NotFound();
            }
            return View(foundCourse);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? courseId)
        {
            Course? foundCourse = _db.Courses.FirstOrDefault(obj => obj.Id == courseId);
            if (ModelState.IsValid && foundCourse != null)
            {
                _db.Courses.Remove(foundCourse);
                _db.SaveChanges();
                TempData["success"] = "Course " + foundCourse.Title + " was removed.";
                return RedirectToAction("Index");
            }
            
            return View(foundCourse);
        }


    }
}
