using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data
{
    /*
    Entity Framework(簡稱為EF)的框架，它主要功能是提供連接關連式資料庫，並對應成為實體（可以把它視為資料物件），進而在程式中操作資料庫時，是以物件導向的方式來設計及操作！
    */
    public class ApplicationDbContext : DbContext //DbContext的角色，則是用來對資料庫操作所提供的一系列方法
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /*
        將實體套入到DbSet，並用它宣告一個參考物件Speakers，
        這個Speakers的參考物件就會有一系列的方法可以對Speaker表格去做CRUD的作！
        */
        public DbSet<Speaker> Speakers { get; set; } = default!; // 驚嘆號，它代表不可為空值
    }
}