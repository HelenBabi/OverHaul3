using Microsoft.AspNetCore.Mvc;
using Overhaul.Application.Models;
using Overhaul.Application.Public;
using Overhaul.Application.Services;
using System.Threading.Tasks;

namespace Overhaul.Web.Controllers
{
    public class OverhaulRegisterController (IRegisterOverhaulService _registerOverhaulService,IMilitaryCardTypeService _militaryCardTypeService,IIsargariTypesServise _isargariTypesServise,IMaritalStatusTypesService _maritalStatusTypesService): Controller
    
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
        public async Task<JsonResponse> save(RegisterOverhaulModel model , CancellationToken ct)
        {
            return  await _registerOverhaulService.AddAsync(model,ct);
        }
    }
}
