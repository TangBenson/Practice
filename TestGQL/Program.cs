using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;
using TestGQL.Data;
using TestGQL.GraphQL;
using TestGQL.GraphQL.Customers;
using TestGQL.GraphQL.Orders;

// 建立 WebApplicationBuilder 物件
var builder = WebApplication.CreateBuilder(args);

//透過 builder.Services 將服務加入 DI 容器
builder.Services
    .AddGraphQLServer() //增加GraphQLServer的服務
    .AddQueryType<Query>()
    .AddType<CustomerType>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<OrderType>()
    .AddSorting()
    .AddFiltering()
    .AddInMemorySubscriptions();


builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GQLConStr"));
});

// 建立 WebApplication 物件，透過 app 設定 Middlewares (HTTP request pipeline)
var app = builder.Build();

app.UseWebSockets(); //增加Websocket的宣告，這部分需要優先宣告

app.MapGet("/", () => "Hello World!");

//增加graphql的路由設定
app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

// 增加Voyager中間件並配置URL
app.UseGraphQLVoyager(new VoyagerOptions { GraphQLEndPoint = "/graphql", },
"/graphql-voyager");

// 啟動 ASP.NET Core 應用程式
app.Run();


/*
由於 .NET 6.0 引入全新的 Top-level statements 語言特性，這段程式碼你看不到任何 using System; 
或 namespace a1 語句，更看不到所謂的 static void Main(string[] args) 方法，因此整份 Program.cs 
看起來非常乾淨清爽，對 .NET 初學者來說非常親切！

另一方面，ASP.NET Core 全新的 Hosting Model 除了大幅簡化 ASP.NET Core 初始化的過程外，
同時也保留了原本 .NET 5.0 原本的 Hosting Model。簡單來說，ASP.NET Core 6.0 的啟動 API 
其實是封裝了 ASP.NET Core 5.0 的所有 API，雖然程式的寫法不同，但是骨子裡是完全相同的，
原本的 ASP.NET Core 5.0 若想升級到 ASP.NET Core 6.0，你原本的 Program.cs 與 Startup.cs 都是不用特別改寫的！
*/