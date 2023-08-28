using HotChocolate;
using HotChocolate.Data;
using TestGQL.Data;
using TestGQL.Models;

// C# 10 全新的 namespace 語法，不用再看到 namespace 的 { } 了！
namespace TestGQL.GraphQL;

[GraphQLDescription("查詢功能")]
public class Query
{
    [GraphQLDescription("這是GetCustomer方法")]
    [UseDbContext(typeof(AppDbContext))]
    [UseSorting]
    [UseFiltering]
    public IQueryable<Customer> GetCustomer([ScopedService] AppDbContext context) //ScopedService表示和資料庫連線有pool概念
    {
        return context.Customers; //這邊的Customers是AppDbContext.cs那宣告的
    }

    //因為建了第2個Model:Order所以加
    [UseDbContext(typeof(AppDbContext))]
    [UseSorting]
    [UseFiltering]
    public IQueryable<Order> GetOrder([ScopedService] AppDbContext context)
    {
        return context.Orders; //這邊的Orders是AppDbContext.cs那宣告的
    }
}
