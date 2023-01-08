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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}