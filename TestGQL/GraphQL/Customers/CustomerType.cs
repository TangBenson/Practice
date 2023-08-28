using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using TestGQL.Data;
using TestGQL.Models;

namespace TestGQL.GraphQL.Customers;

// GraphQL的Type有點類似Model的代理，Model的註解如[GraphQLDescription("會員序號")]可以搬至這邊，讓Models程式比較乾淨(Jerry建議)，也可以隱藏欄位不讓前端抓到
public class CustomerType : ObjectType<Customer>
{
    protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
    {
        descriptor.Description("客戶基本資料FromType");
        descriptor.Field(p => p.Name).Description("會員姓名");
        descriptor.Field(p => p.PhoneNo).Description("會員電話");
        // descriptor.Field(p => p.Id)
        //     .Ignore()
        //     .Description("會員序號");

        descriptor.Field(p => p.Orders) //這邊的Orders是Customer.cs那宣告的
            .ResolveWith<Resolvers>(p => p.GetOrders(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("查詢該客戶的訂單明細");
    }

    //針對特定欄位增加Resolver的自定義處理，讓Customer底下查詢Order，用CustID mapping Id的方式
    //若Customer和Order在不同DB，可以透過定義不同AppDbContext處理，輕鬆做到分表分庫
    private class Resolvers
    {
        public IQueryable<Order> GetOrders([Parent] Customer customer, [ScopedService] AppDbContext context)
        {
            return context.Orders.Where(p => p.CustId == customer.Id); //這邊的Orders是AppDbContext.cs那宣告的
        }
    }
}

