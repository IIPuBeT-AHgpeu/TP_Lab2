namespace TP_Lab2.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int ProductNum { get; set; }
        public Product Product { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
