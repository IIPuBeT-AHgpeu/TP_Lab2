using TPLab2;
using Moq;
using TP_Lab2.Models;
using TP_Lab2.Controllers;

namespace TP_Lab2_Tests
{
    [TestClass]
    public class ShopControllerTest
    {
        [TestMethod]
        public void GetSaleTest_30000_IN_0_05_OUT()
        {
            var controller = new ShopController
                (new ShopContext
                (new Microsoft.EntityFrameworkCore.DbContextOptions<ShopContext>())
                );

            float expected = 0.05F;
            float actual = controller.GetSale(30000F);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetPurchasesTest_person_have_purchases()
        {
            var controller = new ShopController
                (new ShopContext
                (new Microsoft.EntityFrameworkCore.DbContextOptions<ShopContext>())
                );

            var person = new Person() { Id = 1, PurchasesSum = 50597.906F };

            PurchaseViewModel expected = new PurchaseViewModel() { Sale = 0.05F, Sum = 8799, Result = 8359.05F, Products = new List<ProductInPurchase>() };
            PurchaseViewModel actual = controller.GetPurchases(person);
            actual.Products = new List<ProductInPurchase>();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetPurchasesTest_person_dont_have_purchases()
        {
            var controller = new ShopController
                (new ShopContext
                (new Microsoft.EntityFrameworkCore.DbContextOptions<ShopContext>())
                );

            var person = new Person() { Id = 4, PurchasesSum = 0 };

            PurchaseViewModel expected = new PurchaseViewModel() { Sale = 0.01F, Sum = 0, Result = 0F, Products = new List<ProductInPurchase>() };
            PurchaseViewModel actual = controller.GetPurchases(person);
            actual.Products = new List<ProductInPurchase>();

            Assert.AreEqual(expected, actual);
        }
    }
}