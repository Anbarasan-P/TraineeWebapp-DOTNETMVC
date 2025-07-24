//using TraineeWebapp_DOTNETMVC;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TraineeWebapp_DOTNETMVC.Models;

namespace TraineeWebapp_DOTNETMVC.Controllers
{
    public class TraineeController : Controller
    {
        TraineeDAL dal = new TraineeDAL();

        public ActionResult Index()
        {
            return View(dal.GetAllTrainees());
        }

        public ActionResult Details(int id)
        {
            return View(dal.GetTrainee(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Trainee trainee, HttpPostedFileBase PhotoUpload)
        {
            if (PhotoUpload != null && PhotoUpload.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(PhotoUpload.InputStream))
                {
                    trainee.Photo = binaryReader.ReadBytes(PhotoUpload.ContentLength);
                }
            }

            if (ModelState.IsValid)
            {
                dal.InsertTrainee(trainee);
                return RedirectToAction("Index");
            }

            return View(trainee);
        }

        public ActionResult Edit(int id)
        {
            return View(dal.GetTrainee(id));
        }

        [HttpPost]
        public ActionResult Edit(Trainee trainee, HttpPostedFileBase PhotoUpload)
        {
            if (PhotoUpload != null && PhotoUpload.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(PhotoUpload.InputStream))
                {
                    trainee.Photo = binaryReader.ReadBytes(PhotoUpload.ContentLength);
                }
            }

            if (ModelState.IsValid)
            {
                dal.UpdateTrainee(trainee);
                return RedirectToAction("Index");
            }

            return View(trainee);
        }

        public ActionResult Delete(int id)
        {
            return View(dal.GetTrainee(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            dal.DeleteTrainee(id);
            return RedirectToAction("Index");
        }
    }
}
