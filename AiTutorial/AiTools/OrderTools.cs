namespace AiTutorial.AiTools;

public class OrderTools
{
    public async Task<string> GetOrderAsync(int orderId)
    {
        await Task.Delay(10);

        return orderId switch
        {
            1001 => "Order 1001 - Shipped",
            1002 => "Order 1002 - Processing",
            1003 => "Order 1003 - Delivered",
            _ => "Order not found"
        };
    }

    public async Task<List<string>> SearchOrdersAsync(string status)
    {
        await Task.Delay(10);

        var orders = new List<string>
        {
            "Order 1001 - Shipped",
            "Order 1002 - Processing",
            "Order 1003 - Delivered",
            "Order 1004 - Processing"
        };

        return orders
            .Where(x => x.Contains(status, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
