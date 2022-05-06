using HaagsVertaler.Models;
using Microsoft.AspNetCore.Mvc;

namespace HaagsVertaler.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      return View("Home", new TranslationViewModel());
    }

    [HttpPost]
    [Route("~/")]
    public IActionResult IndexPost(TranslationViewModel vm)
    {
      if(!string.IsNullOrEmpty(vm.Source))
        vm.Result = HaagsTranslator.Translator.Translate(vm.Source);
    
      return View("Home", vm);
    }
    
    [HttpGet]
    [Route("~/api")]
    public JsonResult TranslateApi([FromQuery] string text)
    {
      var vm = new TranslationViewModel
      {
        Source = text,
        Result = string.IsNullOrEmpty(text) ? null : HaagsTranslator.Translator.Translate(text)
      }; 
      
      return Json(vm);
    }
  }
}