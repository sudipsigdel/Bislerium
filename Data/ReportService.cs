namespace Bislerium.Data
{
    public class ReportService
    {
        public Dictionary<string, List<ProductSalesQuantity>> getOrderItems(List<OrderItem> orderItems, string TimeFrame)
        {
            List<OrderItem> coffeeList;
            List<OrderItem> addInsList; //contains addins

            if (TimeFrame.Equals("Monthly"))
            {
                coffeeList =  orderItems.Where(item => item.ItemType.ToLower().Equals("coffee") && item.OrderItemDateTime.Year.Equals(DateTime.Now.Year)&& item.OrderItemDateTime.Month.Equals(DateTime.Now.Month)).ToList();
                addInsList = orderItems.Where(item => item.ItemType.ToLower().Equals("add-ins") && item.OrderItemDateTime.Year.Equals(DateTime.Now.Year) && item.OrderItemDateTime.Month.Equals(DateTime.Now.Month)).ToList();
            }
            else
            {
                coffeeList = orderItems.Where(item => item.ItemType.ToLower().Equals("coffee") && item.OrderItemDateTime.Date.Equals(DateTime.Now.Date)).ToList();
                addInsList = orderItems.Where(item => item.ItemType.ToLower().Equals("add-ins") && item.OrderItemDateTime.Date.Equals(DateTime.Now.Date)).ToList();
            }
             //contains coffee

            List<ProductSalesQuantity> MostOrderCoffee = coffeeList
                .GroupBy(item => item.Name)
                .Select(x => new ProductSalesQuantity
                {
                    ProductName = x.Key,
                    Quantity = x.Sum(orderItemQuantity => orderItemQuantity.Quantity)
                }).ToList();

            List <ProductSalesQuantity> MostOrderAddins = addInsList
                .GroupBy(item => item.Name)
                .Select(x => new ProductSalesQuantity
            {
                ProductName = x.Key,
                Quantity = x.Sum(orderItemQuantity => orderItemQuantity.Quantity)
            }).ToList();

            return new Dictionary<string, List<ProductSalesQuantity>>
            {
                { "coffees", MostOrderCoffee.Take(5).ToList() },
                { "add-ins", MostOrderAddins.Take(5).ToList() }
            };
        }

        public List<Order> getOrderList(List<Order> orderList, string TimeFrame)
        {
            List<Order> orderItem;
            if (TimeFrame.Equals("Month"))
            {
                orderItem = orderList.Where(item => item.OrderDateTime.Year.Equals(DateTime.Now.Year) && item.OrderDateTime.Month.Equals(DateTime.Now.Month)).ToList();
            }
            else
            {
                orderItem = orderList.Where(item => item.OrderDateTime.Date.Equals(DateTime.Now.Date)).ToList();
            }
            return orderItem;
        }
    }
}
