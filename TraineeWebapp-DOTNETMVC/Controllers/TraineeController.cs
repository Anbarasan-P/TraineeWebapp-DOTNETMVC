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
            return View(dal.GetTraineeById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                dal.InsertTrainee(trainee);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(dal.GetTraineeById(id));
        }

        [HttpPost]
        public ActionResult Edit(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                dal.UpdateTrainee(trainee);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(dal.GetTraineeById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            dal.DeleteTrainee(id);
            return RedirectToAction("Index");
        }
    }
}