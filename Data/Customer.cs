namespace Bislerium.Data
{
    public class Customer
    {
        public Guid CustomerID { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int OrderCount { get; set; } = 0;
        public bool member {  get; set; } = false;
    }
}
