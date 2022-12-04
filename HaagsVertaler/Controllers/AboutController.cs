using HaagsVertaler.Models;
using Microsoft.AspNetCore.Mvc;

namespace HaagsVertaler.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet]
        [Route("over")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}