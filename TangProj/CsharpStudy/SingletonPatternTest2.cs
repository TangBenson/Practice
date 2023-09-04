
/*
延遲實體化(Lazy Initialization)是一種技巧，可以用來拖延實體化的時間點，到了需要該資源的時候才會去實體化它。

假設Customer類別裡有屬性Orders，存放著大量的訂單資料，這些資料有可能是需要從資料庫提取出來的。
這樣一來，在真正需要訂單資料以前，我們其實不需要先把所有Orders的資料都load進來。
Lazy讓我們可以在需要資料時才去做實體化的動作。
*/
public sealed class Customer
{
    public string? ID { get; set; }
    public string? Name { get; set; }

    // 使用Lazy<T>類別時，我們需要指定物件的型別，然後等到我們存取Lazy<T>.Value屬性之後，該物件才會被實體化出來
    private static Lazy<Order>? lazyInstance;// = new(() => new Order());
    public Customer(string id, string name)
    {
        ID = id;
        Name = name;
        // 定義 Lazy<T> 的初始化委派，當訪問 Order 屬性時執行
        lazyInstance = new Lazy<Order>(() => LoadOrderFromDatabase(ID));
    }
    public Order Order => lazyInstance.Value;
    
    // 模擬從資料庫中加載 Order 的方法
    private Order LoadOrderFromDatabase(string customerId)
    {
        // 在這裡可以實際查詢資料庫，這裡只是一個示例
        Thread.Sleep(2000); // 模擬耗時的資料庫查詢
        return new Order { OrderNumber = "12345", Price = "999" };
    }

    static void Main()
    {
        Customer customer  = new("850720","珮綺");
        // 在這裡訪問 Order 屬性，它將觸發從資料庫加載
        Console.WriteLine($"Customer ID: {customer.ID}, Name: {customer.Name}, Order Number: {customer.Order.OrderNumber}");
    }
}

public class Order
{
    public string? OrderNumber { get; set; }
    public string? Price { get; set; }
}