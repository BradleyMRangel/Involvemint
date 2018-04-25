using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Repository;
using WebApplication1.ViewModel.MarketingViewModel;

namespace WebApplication1.Controllers
{
    public class MarketingMaterialController : Controller
    {
        public ActionResult Index(string id)
        {
            var vm = new MarketingViewModel(id);
            vm.HandleRequest();
            return View(vm);
        }


        

        [HttpPost]
        public ActionResult IndexAdmin(MarketingViewModel vm)
        {
            vm.HandleRequest();
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values
            ModelState.Clear();
            return View("Index", vm);
            // return View(vm);
        }

        public ActionResult IndexAdmin(string id, bool role)
        {
            var vm = new MarketingViewModel(id, role);
            vm.HandleRequest();
            return View("Index",vm);
        }
        [HttpPost]
        public  ActionResult Index(MarketingViewModel vm)
        {
             vm.HandleRequest();
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values
            ModelState.Clear();
            return View(vm);
        }

        [HttpPost]

        public ActionResult UserIndex(string id)
        {
            var vm = new MarketingViewModel(id);
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        public ActionResult UserIndex(MarketingViewModel vm)
        {
            vm.HandleRequest();
            // NOTE: Must clear the model state in order to bind
            //       the @Html helpers to the new model values
            ModelState.Clear();
            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile()
        {
            var blobName = ""; // the name associated with the blob image
            try
            {
                var blobAdapter = new BlogStorageAdapter(); // adapter to the blog repository code
                foreach (string file in Request.Files)
                    // this is populated by the html5 browser when the user selects a file and contains the file to upload
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        if (fileContent.ContentLength > 250000) // no images allowed over 250K
                            throw new Exception();
                        // get a stream
                        var stream = fileContent.InputStream;

                        var imageName = string.Format("mkt-{0}{1}",
                            Guid.NewGuid(),
                            Path.GetExtension(fileContent.FileName));  // provide a unique name for the blob image
                        var blockBlob = blobAdapter.GetBlockBlob(imageName);
                        blobName = blockBlob.Uri.ToString();
                        await blobAdapter.Add(blockBlob.Name, stream); // ask the blobAdapter to store the image
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(blobName);// return value to the ajax call
        }
    }
}