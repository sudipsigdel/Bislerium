namespace Bislerium.Data
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public String EmployeeRole { get; set; }
        public DateTime OrderDateTime { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; }
        public Double OrderTotalAmount { get; set; }
    }
}
