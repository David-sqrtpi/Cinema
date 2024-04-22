using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public string Index()
        //{
        //    return "This is my default action...";
        //    //return View();
        //}
        //public string Welcome(string name, int numTimes = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        //}

        //public string Welcome(string name, int ID = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        //}

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}
