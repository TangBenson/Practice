using System.ComponentModel.DataAnnotations;

/*
Spearker.cs的角色是一個實體，它會對應到資料庫中的Speaker表格(Table)，
而其中的每個資料成員也都會對應到表格中的欄位。在預設的情況下，它們名稱會是一樣的（當然也可以改變）
*/
namespace GraphQL.Data;
public class Speaker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [StringLength(4000)]
        public string? Bio { get; set; }

        [StringLength(1000)]
        public virtual string? WebSite { get; set; }
    }