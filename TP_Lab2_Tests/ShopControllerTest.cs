using TPLab2;
using Moq;
using Moq.EntityFrameworkCore;
using TP_Lab2.Models;
using TP_Lab2.Controllers;
using Microsoft.EntityFrameworkCore;

namespace TP_Lab2_Tests
{
    [TestClass]
    public class ShopControllerTest
    {
        [TestMethod]
        public void GetSaleTest_30000_IN_0_05_OUT()
        {
            var controller = new ShopController(new ShopContext(new DbContextOptions<ShopContext>()));

            float expected = 0.05F;
            float actual = controller.GetSale(30000F);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSaleTest_0_IN_0_01_OUT()
        {
            var controller = new ShopController(new ShopContext(new DbContextOptions<ShopContext>()));

            float expected = 0.01F;
            float actual = controller.GetSale(0F);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateResultsTest_out_sum_10000_sale_0_01_res_9900()
        {
            var controller = new ShopController(new ShopContext(new DbContextOptions<ShopContext>()));

            List<ProductInPurchase> products = new List<ProductInPurchase>();
            products.Add(new ProductInPurchase() { Product = new Product() { Price = 5000 }, Count = 1 });
            products.Add(new ProductInPurchase() { Product = new Product() { Price = 1000 }, Count = 3 });
            products.Add(new ProductInPurchase() { Product = new Product() { Price = 2000 }, Count = 1 });

            PurchaseViewModel expected = new PurchaseViewModel() 
            { 
                Products = products, Sum = 10000, Sale = 0.01F, Result = 9900F 
            };

            PurchaseViewModel actual = controller.CalculateResults(products, 9999F);

            Assert.AreEqual(expected.Result, actual.Result);
        }

        [TestMethod]
        public void CalculateResultsTest_out_sum_0_sale_0_03_res_0()
        {
            var controller = new ShopController(new ShopContext(new DbContextOptions<ShopContext>()));

            List<ProductInPurchase> products = new List<ProductInPurchase>();

            PurchaseViewModel expected = new PurchaseViewModel()
            {
                Products = products,
                Sum = 0,
                Sale = 0.01F,
                Result = 0F
            };

            PurchaseViewModel actual = controller.CalculateResults(products, 9999F);

            Assert.AreEqual(expected.Result, actual.Result);
        }

        [TestMethod]
        public void CalculateResultsTest_out_sum_10000_sale_0_05_res_9500()
        {
            var controller = new ShopController(new ShopContext(new DbContextOptions<ShopContext>()));

            List<ProductInPurchase> products = new List<ProductInPurchase>();
            products.Add(new ProductInPurchase() { Product = new Product() { Price = 5000 }, Count = 1 });
            products.Add(new ProductInPurchase() { Product = new Product() { Price = 2500 }, Count = 2 });

            PurchaseViewModel expected = new PurchaseViewModel()
            {
                Products = products,
                Sum = 10000,
                Sale = 0.05F,
                Result = 9500F
            };

            PurchaseViewModel actual = controller.CalculateResults(products, 60000F);

            Assert.AreEqual(expected.Result, actual.Result);
        }
    }
}