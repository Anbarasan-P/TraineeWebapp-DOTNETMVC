using System.IO;
using System.Web;
using System.Web.Mvc;
using TraineeWebapp_DOTNETMVC.Models;

namespace TraineeWebapp_DOTNETMVC.Controllers
{
    public class TraineeController : Controller
    {
        TraineeDAL dataAccessLayer = new TraineeDAL();

        public ActionResult Index()
        {
            return View(dataAccessLayer.GetAllTrainees());
        }

        public ActionResult Details(int id)
        {
            return View(dataAccessLayer.GetTrainee(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                else {
                    trainee.Photo = null; // Handle case where no photo is uploaded

                }
                dataAccessLayer.InsertTrainee(trainee);
                return RedirectToAction("Index");
            }
            return View(trainee);
        }

        public ActionResult Edit(int id)
        {
            return View(dataAccessLayer.GetTrainee(id));
        }

        public ActionResult Edit(Trainee trainee, HttpPostedFileBase PhotoFile)
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
                else
                {
            
                    var existingTrainee = dataAccessLayer.GetTrainee(trainee.TraineeID);
                    trainee.Photo = existingTrainee.Photo;
                }
                dataAccessLayer.UpdateTrainee(trainee);
                return RedirectToAction("Index");
            }
            return View(trainee);
        }

        public ActionResult Delete(int id)
        {
            return View(dataAccessLayer.GetTrainee(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            dataAccessLayer.DeleteTrainee(id);
            return RedirectToAction("Index");
        }
    }
}
