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

    [HttpPost]
    [Route("~/")]
    public ActionResult IndexPost(TranslationViewModel vm)
    {
      if(!string.IsNullOrEmpty(vm.Source))
        vm.Result = HaagsTranslator.Translator.Translate(vm.Source);

      return View("Home", vm);
    }
  }
}