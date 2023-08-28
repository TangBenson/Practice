using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using TestGQL.Data;
using TestGQL.GraphQL.Customers;
using TestGQL.GraphQL.Orders;
using TestGQL.Models;

namespace TestGQL.GraphQL;

[GraphQLDescription("修改功能 - 鳩咪")]
public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCustomerPayload> AddCustomerAsync(
        AddCustomerInput input,
        [ScopedService] AppDbContext context,
        [Service] ITopicEventSender eventSender, //增加訂閱相關參數，讓資料新增後能通知訂閱者
        CancellationToken cancellationToken
        )
    {
        var customer = new Customer
        {
            Name = input.name,
            PhoneNo = input.phoneno
        };
        context.Customers.Add(customer);
        // await context.SaveChangesAsync();
        //修改增加訂閱相關參數
        await context.SaveChangesAsync(cancellationToken);
        //訂閱觸發的設定
        await eventSender.SendAsync(nameof(Subscription.OnCustomerAdded), customer, cancellationToken);

        return new AddCustomerPayload(customer);
    }


    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddOrderPayload> AddOrderAsync(
        AddOrderInput input,
        [ScopedService] AppDbContext context
        )
    {
        var order = new Order
        {
            OrderNo = input.orderno,
            OrderDate = input.orderdate,
            CustId = input.custid
        };
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        return new AddOrderPayload(order);
    }
}