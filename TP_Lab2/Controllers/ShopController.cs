using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP_Lab2.Models;

namespace TP_Lab2.Controllers
{
    public class ShopController : Controller
    {
        public ShopController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string login, string password, string action)
        {
            //Actions
            if (action == "log")
            {
                //login actions
                return RedirectToAction("Catalog");
            }
            else if (action == "reg")
            {
                //registration actions
                return View();
            }
            else
            {
                //write error info
                return View();
            }
        }

        public IActionResult Purchase()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}