using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using TestGQL.Data;
using TestGQL.Models;

namespace TestGQL.GraphQL.Orders;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Description("訂單基本資料FromType");
        descriptor.Field(p => p.OrderNo).Description("訂單號碼");
        descriptor.Field(p => p.OrderDate).Description("訂單日期");
        descriptor.Field(p => p.CustId).Description("客戶Id");
        descriptor.Field(p => p.Id).Ignore();

        descriptor.Field(p => p.Customer)
            .ResolveWith<Resolvers>(p => p.GetCustomers(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("查詢該訂單的客戶何許人也");
    }

    private class Resolvers
    {
        public IQueryable<Customer> GetCustomers([Parent] Order order, [ScopedService] AppDbContext context)
        {
            return context.Customers.Where(p => p.Id == order.CustId);
        }
    }
}
