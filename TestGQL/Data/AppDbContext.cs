using TestGQL.Models;
using Microsoft.EntityFrameworkCore;

namespace TestGQL.Data;

public class AppDbContext : DbContext // DbContext 是 EF Core 跟資料庫溝通的主要類別
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        /*
        base(options) 是使用 DbContext 類別的建構子來初始化父類別。這裡的 options 是一個 DbContextOptions 物件，
        用來配置和設定 DbContext 的選項。當您建立 AppDbContext 的實例時，您需要提供 DbContextOptions 物件作為參數，
        以指定資料庫連線字串、資料庫提供者和其他選項。然後這個 options 物件會透過 base(options) 傳遞給 DbContext 的建構子。
        簡單來說，base(options) 就是將 DbContextOptions 物件傳遞給父類別 DbContext 的建構子，以便在建立 AppDbContext 實例時初始化父類別的狀態。
        這種設計模式稱為「呼叫基底類別建構子」（calling the base class constructor）。這讓您可以在子類別的建構子中調用父類別的建構子，
        以確保在建立子類別實例時，父類別的相關初始化邏輯也能被執行。
        */
    }

    // Migration的檔案是依照 Model的定義及 AppDbContext中的內容，來產生相關的資料表 Sql Script
    public DbSet<Customer> Customers { get; set; } //EF Core會用 Customer這個 model去 DB裡面 set Customers這個 Table的內容
    public DbSet<Order> Orders { get; set; } //因為建了第2個 Model:Order所以加這一行



    //因為建了第2個Model:Order所以加，設定訂單與客戶關聯。拿掉這段會造成order無法query，但customer一樣可以
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Customer>()
            .HasMany(p => p.Orders) //這邊的 Orders 是 Customer.cs 那宣告的
            .WithOne(p => p.Customer) //這邊的 Customer 是 Order.cs 那宣告的
            .HasForeignKey(p => p.Id); //這邊的 Id 是 Order.cs 那宣告的
        modelBuilder
            .Entity<Order>()
            .HasOne(p => p.Customer) //這邊的 Customer 是 Order.cs 那宣告的
            .WithMany(p => p.Orders) //這邊的 Orders 是 Customer.cs 那宣告的
            .HasForeignKey(p => p.CustId);
    }
}
