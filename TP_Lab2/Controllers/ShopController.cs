using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP_Lab2.Models;

namespace TP_Lab2.Controllers
{
    public class ShopController : Controller
    {
        public ShopController()
        {
            //add dbcontext
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string login, string password, string action)
        {
            Person? person = new Person();//find person in DB
            person.Password = "123";//test
            person.Id = 1;//test

            if (action == "reg")
            {
                if (person == null)
                {
                    //create new person
                    ViewBag.MessageColor = "black";
                    ViewBag.Message = "Вы успешно зарегистрировались!";
                }
                else
                {
                    ViewBag.Login = login;
                    ViewBag.Password = password;
                    ViewBag.MessageColor = "red";
                    ViewBag.Message = "Пользователь с таким логином уже существует!";

                }
                return View();
            }
            else
            {
                if (person == null)
                {
                    ViewBag.Login = login;
                    ViewBag.Password = password;
                    ViewBag.MessageColor = "red";
                    ViewBag.Message = "Пользователь с таким логином не найден!";
                    
                    return View();
                }
                else
                {
                    if (person.Password == password)
                    {
                        //redirect
                        CatalogViewModel model = new CatalogViewModel()
                        {
                            PersonId = person.Id,
                            //Products
                        };
                        return RedirectToAction("Catalog", model);
                    }
                    else
                    {
                        ViewBag.Login = login;
                        ViewBag.Password = password;
                        ViewBag.MessageColor = "red";
                        ViewBag.Message = "Неверный пароль!";

                        return View();
                    }
                }
            }
           
        }

        public IActionResult Purchase()
        {
            //
            return View();
        }

        public IActionResult Catalog(CatalogViewModel model)
        {
            //
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}