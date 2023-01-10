using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP_Lab2.Models;

namespace TP_Lab2.Controllers
{
    public class ShopController : Controller
    {
        private ShopContext _db;
        public ShopController(ShopContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string login, string password, string action)
        {
            Person? person = _db.Persons.FirstOrDefault(p => p.Login == login);

            if (action == "reg")
            {
                if (person == null)
                {
                    _db.Persons.Add(new Person() { Login = login, Password = password, Name = "", PurchasesSum = 0 });
                    _db.SaveChanges();
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
            ViewBag.PersonId = personId;

            Person person = _db.Persons.FirstOrDefault(p => p.Id == personId);
            
            PurchaseViewModel model = ConstructPurchaseViewModel(person);

            return View(model);
        }

        [HttpPost]
        public IActionResult Purchase(int productId, int personId)
        {
            Purchase pur = _db.Purchases.FirstOrDefault(p => p.ProductId == productId && p.PersonId == personId);
            _db.Purchases.Remove(pur);
            _db.SaveChanges();

            return RedirectToAction("Purchase", new { personId = personId });
        }

        [HttpGet]
        public IActionResult DeletePurchase(int personId)
        {
            Person person = _db.Persons.FirstOrDefault(p => p.Id == personId);

            PurchaseViewModel model = ConstructPurchaseViewModel(person);

            Purchase[] purs = _db.Purchases.Where(p => p.PersonId == personId).ToArray();
            _db.Purchases.RemoveRange(purs);
            person.PurchasesSum += model.Result;

            _db.SaveChanges();

            return RedirectToAction("Catalog", new { personId = personId });
        }

        public IActionResult Catalog(int personId)
        {
            ViewBag.PersonId = personId.ToString();

            List<Product> products = _db.Products.ToList();

            return View(products);
        }

        [HttpPost]
        public IActionResult Catalog(int num, int productId, int personId)
        {
            Purchase? exist = _db.Purchases.FirstOrDefault(p => p.ProductId == productId && p.PersonId == personId);

            if (exist == null)
            {
                _db.Purchases.Add(new Purchase() { PersonId = personId, ProductId = productId, ProductNum = num });
            }
            else
            {
                exist.ProductNum += num;
            }
            _db.SaveChanges();

            return RedirectToAction("Catalog", new { personId = personId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public float GetSale(float sum)
        {
            if (sum < 10000) return 0.01F;
            else if (sum >= 10000 && sum < 30000) return 0.03f;
            else return 0.05F;
        }

        private PurchaseViewModel ConstructPurchaseViewModel(Person person)
        {           
            var purchaseList = from purchase in _db.Purchases
                               join product in _db.Products on purchase.ProductId equals product.Id
                               where purchase.PersonId == person.Id
                               select new ProductInPurchase()
                               {
                                   Product = product,
                                   Count = purchase.ProductNum
                               };

            return CalculateResults(purchaseList.ToList(), person.PurchasesSum);
        }

        public PurchaseViewModel CalculateResults(List<ProductInPurchase> products, float personSum)
        {
            PurchaseViewModel model = new PurchaseViewModel() { Products = products };

            model.Sale = GetSale(personSum);
            model.CalculateResultParameters();

            return model;
        }
    }
}