using Microsoft.AspNetCore.Mvc;
using Overhaul.Application.Models;
using Overhaul.Application.Public;
using Overhaul.Application.Services;
using System.Threading.Tasks;

namespace Overhaul.Web.Controllers
{
    public class OverhaulRegisterController 
        (
        IRegisterOverhaulService _registerOverhaulService,
        IMilitaryCardTypeService _militaryCardTypeService,
        IIsargariTypesServise _isargariTypesServise,
        IMaritalStatusTypesService _maritalStatusTypesService,
        IWebHostEnvironment webHostEnvironment
        ) : Controller
    
    {
        public IActionResult Index()
        {
                var model = new RegisterOverhaulModel();
            model.SelectListMilitaryCardType = _militaryCardTypeService.GetSelectList();
            model.SelectListIsargariType = _isargariTypesServise.GetSelectList();
            model.SelectListMaritalStatusType = _maritalStatusTypesService.GetSelectList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Save(RegisterOverhaulModel model , CancellationToken ct)
        {
            var result =   await _registerOverhaulService.AddAsync(model,ct);
            TempData["RegisterId"] = result;
            return RedirectToAction("ReqisterResult", "OverhaulRegister", new { id= result });
        }
        public async Task<IActionResult> SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";

            try
            {
                var nationality = Request.Form.Keys.FirstOrDefault(); // دریافت مقدار ورودی

                if (string.IsNullOrEmpty(nationality))
                {
                    return BadRequest(new { Message = "inspectionId is required." });
                }

                var files = Request.Form.Files;
                if (files.Count == 0)
                {
                    return BadRequest(new { Message = "No files received." });
                }

                string uploadPath = Path.Combine(webHostEnvironment.ContentRootPath, "Attachment", nationality);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        fName = file.FileName;
                        var filePath = Path.Combine(uploadPath, fName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                return StatusCode(500, new { Message = "Error in saving file", Error = ex.Message });
            }

            if (isSavedSuccessfully)
            {
                return Ok(new { Message = fName });
            }
            else
            {
                return StatusCode(500, new { Message = "Error in saving file" });
            }
        }
        //public ActionResult RemoveUploadedFile(string filename, string inspectionId)
        //{
        //    bool isSavedSuccessfully = true;
        //    string fName = "";


        //    try
        //    {

        //        var originalDirectory = new DirectoryInfo(string.Format("{0}Attachment\\Inspection", Server.MapPath(@"\")));

        //        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), inspectionId);

        //        System.IO.File.Delete(System.IO.Path.Combine(pathString, filename));


        //    }
        //    catch (Exception ex)
        //    {
        //        isSavedSuccessfully = false;
        //    }


        //    if (isSavedSuccessfully)
        //    {
        //        return Json(new { Message = fName });
        //    }
        //    else
        //    {
        //        return Json(new { Message = "Error in saving file" });
        //    }
        //}
        public IActionResult ReqisterResult()
        {

            return View();
        }
    }
}
