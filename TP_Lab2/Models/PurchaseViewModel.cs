namespace TP_Lab2.Models
{
    public class PurchaseViewModel
    {
        public List<ProductInPurchase> Products { get; set; }

        public int Sum { get; set; }
        public float Sale { get; set; }
        public float Result { get; set; }

        public void CalculateResultParameters()
        {
            Sum = 0;
            foreach (var product in Products)
            {
                Sum += product.Product.Price * product.Count;
            }

            Result = (float)Sum - (float)Sum * Sale;
        }

    }
}
