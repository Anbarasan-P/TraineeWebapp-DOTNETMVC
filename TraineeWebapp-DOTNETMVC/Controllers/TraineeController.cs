using System.IO;
using System.Web;
using System.Web.Mvc;
using TraineeWebapp_DOTNETMVC.Models;

namespace TraineeWebapp_DOTNETMVC.Controllers

{
    [Authorize] // Ensure that all actions require authentication // controller-wide authorization
    public class TraineeController : Controller // TraineeController class
    {
        TraineeDAL dataAccessLayer = new TraineeDAL();// Data access layer instance

        [Authorize(Roles = "Admin,Trainee")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View(dataAccessLayer.GetAllTrainees());
            }
            else
            {
                int traineeId = (int)Session["TraineeID"];
                var trainee = dataAccessLayer.GetTraineeById(traineeId);
                return View(new[] { trainee });
            }
        }

        [Authorize(Roles = "Admin,Trainee")]
        public ActionResult Details(int id)
        {
            // Trainee can view only their own details
            if (User.IsInRole("Trainee"))
            {
                int traineeId = (int)Session["TraineeID"];
                if (traineeId != id)
                    return new HttpUnauthorizedResult();
            }

            return View(dataAccessLayer.GetTraineeById(id));
        }

        // Only Admin can create trainees
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Trainee trainee, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(PhotoFile.InputStream))
                    {
                        trainee.Photo = binaryReader.ReadBytes(PhotoFile.ContentLength);
                    }
                }
                dataAccessLayer.InsertTrainee(trainee);
                return RedirectToAction("Index");
            }
            return View(trainee);
        }

        // Only Admin can access Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(dataAccessLayer.GetTraineeById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Trainee trainee, HttpPostedFileBase PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(PhotoFile.InputStream))
                {
                    trainee.Photo = binaryReader.ReadBytes(PhotoFile.ContentLength);
                }
            }
            else
            {
                var existingTrainee = dataAccessLayer.GetTraineeById(trainee.TraineeID);
                trainee.Photo = existingTrainee.Photo;
            }

            if (ModelState.IsValid)
            {
                dataAccessLayer.UpdateTrainee(trainee);
                return RedirectToAction("Index");
            }
            return View(trainee);
        }

        // Only Admin can access Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(dataAccessLayer.GetTraineeById(id));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            dataAccessLayer.DeleteTrainee(id);
            return RedirectToAction("Index");
        }
    }
}
