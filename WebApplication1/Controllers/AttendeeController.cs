using System;
using System.IO;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Repository;
using WebApplication1.ViewModel.AttendeeViewModel;

namespace WebApplication1.Controllers
{
    public class AttendeeController : Controller
    {
        public ActionResult Index(string id)
        {
            var vm = new AttendeeViewModel(id);
            vm.HandleRequest();
            return View(vm);
        }

        public ActionResult IndexAdmin(string id, bool role)
        {
            var vm = new AttendeeViewModel(id,role);
            vm.HandleRequest();
            return View("Index",vm);
        }

        [HttpPost]
        public ActionResult IndexAdmin(AttendeeViewModel vm)
        {
            vm.HandleRequest();
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values
            ModelState.Clear();
            return View("Index",vm);
            // return View(vm);
        }
        [HttpPost]
        public ActionResult Index(AttendeeViewModel vm)
        {
            vm.HandleRequest();
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values
            ModelState.Clear();
            return View(vm);
           // return View(vm);
        }

    }
}