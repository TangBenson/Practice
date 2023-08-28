using System.ComponentModel.DataAnnotations;

namespace TestGQL.Models;

// [GraphQLDescription("會員基本資料檔")] //呈現在voyager
public class Customer
{
    [Key]
    // [GraphQLDescription("會員序號")]
    public int Id { get; set; }

    [Required]
    // [GraphQLDescription("會員姓名")]
    public string? Name { get; set; }

    // [GraphQLDescription("會員電話")]
    public string? PhoneNo { get; set; }

    // 因為建了第2個Model:Order所以加這一行，宣告的變數用於AppDbContext.cs
    // 打api用的變數名稱需與此變數名稱相同
    // 查詢Customer時可以一併帶出該Customer的所有Order 
    // [GraphQLDescription("會員訂單資料")]
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
