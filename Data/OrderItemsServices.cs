using System.Text.Json;

namespace Bislerium.Data
{
    public class OrderItemsServices
    {
        public void AddItemInOrderList(List<OrderItem> _orderItems, Guid itemID, String ItemType, Double ItemPrice, string name)
        {
            // Check if the item already exists in the order
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.ItemID == itemID && x.ItemType == ItemType);

            if (orderItem != null)
            {
                
                orderItem.Quantity++;
                
                orderItem.TotalPrice = orderItem.Quantity * ItemPrice;
                return;
            }

            orderItem = new()
            {
                Name = name,
                ItemID = itemID,
                ItemType = ItemType,
                Quantity = 1,
                Price = ItemPrice,
                TotalPrice = ItemPrice
            };
            
            _orderItems.Add(orderItem);
        }

        public void DeleteItemInOrderItemsList(List<OrderItem> _orderItems, Guid orderItemID)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }
        }

        public void QuantityOfItemList(List<OrderItem> _orderItems, Guid orderItemID, String action)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                if (action == "add")
                {
                    orderItem.Quantity++;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
                
                else if (action == "sub" && orderItem.Quantity > 1)
                {
                    orderItem.Quantity--;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
                
                else if (action == "sub" && orderItem.Quantity == 1)
                {
                    _orderItems.Remove(orderItem);
                }
            }
        }

        public List<OrderItem> GetOrderListFromJsonFile()
        {
            string orderListFilePath = AppUtils.GetOrderItemListPath();

            if (!File.Exists(orderListFilePath))
            {
                return new List<OrderItem>();
            }

            var json = File.ReadAllText(orderListFilePath);

            return JsonSerializer.Deserialize<List<OrderItem>>(json);
        }

        public void SaveOrderList(OrderItem order)
        {
            List<OrderItem> orders = GetOrderListFromJsonFile();
            orders.Add(order);

            string appDataDirPath = AppUtils.GetAppDirectoryPath();
            string orderListFilePath = AppUtils.GetOrderItemListPath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(orderListFilePath, json);
        }
    }
}