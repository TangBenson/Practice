using System.ComponentModel.DataAnnotations;

namespace TestGQL.Models;

// [GraphQLDescription("訂單資料檔")] //呈現在voyager
public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? OrderNo { get; set; }
    public string? OrderDate { get; set; }
    public int? CustId { get; set; }
    public Customer? Customer { get; set; }
}
