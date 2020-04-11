using System.Web.Mvc;
using HaagsVertaler.Models;

namespace HaagsVertaler.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet]
    [Route("~/", Name = "default")]
    public ActionResult Index()
    {
      return View("Home", new TranslationViewModel());
    }

    //[HttpPost]
    //[Route("~/")]
    //public ActionResult IndexPost(TranslationViewModel vm)
    //{
    //  if(!string.IsNullOrEmpty(vm.Source))
    //    vm.Result = HaagsTranslator.Translator.Translate(vm.Source);

    //  return View("Home", vm);
    //}

    //[HttpGet]
    //[Route("~/api")]
    //public JsonResult TranslateApi(string text)
    //{
    //  return new JsonNetResult
    //  {
    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
    //    Data = new TranslationViewModel
    //    {
    //      Source = text,
    //      Result = string.IsNullOrEmpty(text) ? null : HaagsTranslator.Translator.Translate(text)
    //    }
    //  };
    //}
  }
}