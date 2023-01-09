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
                        return RedirectToAction("Catalog", "Shop", new { personId = person.Id });
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

        public IActionResult Purchase(int personId)
        {
            Console.WriteLine($"It works! {personId}");//debug
            //
            return View();
        }

        public IActionResult Catalog(int personId)
        {
            //
            ViewBag.PersonId = personId.ToString();

            List<Product> products = new List<Product>();//get products from db
            products.Add(new Product() { Name = "Gachi", Price = 300, Id = 2 });//test
            products.Add(new Product() { Name = "Muchi", Price = 300, Id = 48 });//test

            return View(products);
        }

        [HttpPost]
        public IActionResult Catalog(int num, int productId, string personId)
        {
            //
            ViewBag.PersonId = personId.ToString();

            //add products to purchase
            Console.WriteLine($"{num};;;{productId};;;{personId}");//debug

            return RedirectToAction("Catalog", new { personId = personId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}