using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebApplication1.Commands.CompositeEventCommands;
using WebApplication1.Model;
using WebApplication1.ViewModel.EventViewModels;
using System;


namespace WebApplication1.Controllers
{
    public class EventController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            ViewBag.IsValid = true;
            if (UserManager.IsValid(user.Username, user.Password))
            {
                string role = UserManager.GetRole(user.Username);
                var ident = new ClaimsIdentity(
                    new[]
                    {
                        // adding following 2 claim just for supporting default  provider
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                            "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, role)
                    },
                    DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = false }, ident);
                //return RedirectToAction("Index");

                if (role == "Admin")
                {
                    return RedirectToAction("IndexAdmin");
                }
                if(role == "user"){ 
                    return RedirectToAction("Index");
                }

            }
            // invalid username or password
            ViewBag.IsValid = false;
            ModelState.AddModelError("", "invalid username or password");
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.IsValid = true;
            return View();
        }

        //[Authorize]
        public ActionResult Index()
        {
            var vm = new EventViewModel();
            vm.HandleRequest();
            return View(vm);
        }

        
       //[Authorize]
       // //*******************************************  Add this method to set the ViewModel "arole variable"
        public ActionResult IndexAdmin()
        {  // ****************************************** this set the viewmodel admin role (
            var vm = new EventViewModel(true);
            vm.HandleRequest();
            return View("Index",vm);
        }

        [HttpPost]
        public ActionResult IndexAdmin(EventViewModel vm)
        {  // ****************************************** this set the viewmodel admin role (
            
            vm.IsValid = ModelState.IsValid;   // added *****************************
            vm.HandleRequest();
           
            // redirect if we need to add marketing material to the product
            if (vm.EventCommand.Equals("updateMaterials"))
                return RedirectToAction("IndexAdmin", "MarketingMaterial", new { id = vm.EventArgument,role = true });
            else if (vm.EventCommand.Equals("updateAttendance"))
                return RedirectToAction("IndexAdmin", "Attendee", new { id = vm.EventArgument,role =true });
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values if modelstate is valid
            if (vm.IsValid)   // added *******************************
            {
                ModelState.Clear();
            }
            return View("Index",vm);
        }
        //[Authorize]
        [HttpPost]
        public ActionResult Index(EventViewModel vm)
        {
          
            vm.IsValid = ModelState.IsValid;   // added *****************************
            vm.HandleRequest();

            // redirect if we need to add marketing material to the product
            if (vm.EventCommand.Equals("updateMaterials"))
                return RedirectToAction("Index", "MarketingMaterial", new {id = vm.EventArgument});
                        else if (vm.EventCommand.Equals("updateAttendance"))
                return RedirectToAction("Index","Attendee" ,new {id = vm.EventArgument});
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values if modelstate is valid
            if (vm.IsValid)   // added *******************************
            {
                ModelState.Clear();
            }
            return View(vm);
        }



        [Authorize]
        [HttpPost]


        public ActionResult AutoComplete(string term)
        {
            var autocmpcmd = EventCommand.AutoCompleteCommand;
            autocmpcmd.Execute(null, new Event {EventName = term});
            var result = autocmpcmd.GetSortedList();
            var model = (from product in result
                select new {label = product.EventName}).Take(10);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowCurrentEvents()
        {
            var currenteventCmd = EventCommand.ShowCurrrentEventsCommand;
            currenteventCmd.Execute(null, new Event { EventDate = DateTime.Today });
            var result = currenteventCmd.GetSortedList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MaxAttendance(string term)
        {
            if (term != null)
            {
                var maxAttendanceCmd = EventCommand.MaxAttendanceCommand;
                maxAttendanceCmd.Execute(null, new Event { EventAttendance = int.Parse(term) });
                var result = maxAttendanceCmd.GetSortedList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}