using MongodbRealmSync;
using MongodbRealmSync.Models;
using Realms;
using Realms.Sync;
using Realms.Logging;
using MongoDB.Bson;

Console.WriteLine($"---START---{Environment.CurrentManagedThreadId}");

#region 測試一
//step <1>
App app = App.Create("application-0-gyubp");
var credential = Credentials.Anonymous();
try
{
    await app.LogInAsync(credential);
}
catch (Exception ex)
{
    Console.WriteLine($"Login failed: {ex.Message}");
}
// Logger.LogLevel = LogLevel.Trace; // 導入Realm log
// Logger.Default = Logger.Function((message) =>
// {
//     Console.WriteLine(message);
// });

//step <2>
var config = new FlexibleSyncConfiguration(app.CurrentUser);
// {
//     PopulateInitialSubscriptions = (realm) =>
//     {
//         var myItems = realm.All<Car>();
//         realm.Subscriptions.Add(myItems);
//     }
// };

//step <3>
Realm.DeleteRealm(config);

//step <4>
//創建了一個新的 Realm 實例
var realm = Realm.GetInstance(config);
//向 Realm 添加了一個訂閱，這和上面的PopulateInitialSubscriptions意思一樣
realm.Subscriptions.Update(() =>
{
    var myCars = realm.All<MotorRent>();//.Where(t => t.Name == "Clifford" && t.Age > 5);
    realm.Subscriptions.Add(myCars);
});
Console.WriteLine($"---ID1---{Environment.CurrentManagedThreadId}");

//step <5>
realm.Subscriptions.WaitForSynchronizationAsync(); //等待所有訂閱的同步操作完成
// await myCars.SubscribeAsync(); //不知道三小
Console.WriteLine($"---ID2---{Environment.CurrentManagedThreadId}");

//step <6>
// realm.Refresh();
Console.WriteLine($"---ID3---{Environment.CurrentManagedThreadId}");

//step <7> - Realm有資料異動會觸發此通知處理程序，但該處理程序不會收到有關變更的具體信息
realm.RealmChanged += (sender, eventArgs) =>
{
    Console.WriteLine("AAAAAAA");
};

//step <8> - 在 Realm 內的特定物件上註冊通知處理程序
// var artist = realm.All<Car>().FirstOrDefault();
// artist!.PropertyChanged += (sender, eventArgs) =>
// {
//     var changedProperty = eventArgs.PropertyName!;
//     Console.WriteLine(
//         $@"New value set for 'artist':
//         '{changedProperty}' is now {artist.GetType().GetProperty(changedProperty)!.GetValue(artist)}");
// };

//step <9> - 若要停止就呼叫token.Dispose()
var token = realm.All<MotorRent>()
    .SubscribeForNotifications((sender, changes) =>
    {
        Console.WriteLine("狂浪是一種態度");
        // 没有有效的更改通知
        if (changes == null)
        {
            Console.WriteLine("changes is null");
            return; //中止当前函数的执行并立即退出
        }
        // Handle individual changes
        foreach (var i in changes.DeletedIndices)
        {
            Console.WriteLine($"delete event");
        }
        foreach (var i in changes.InsertedIndices)
        {
            // Console.WriteLine($"Insert event");
        }
        foreach (var i in changes.NewModifiedIndices)
        {
            Console.WriteLine($"Modify event");
        }
        if (changes.IsCleared)
        {
            Console.WriteLine("changes is cleared");
            return;
        }
    });

int count = 0;
while (true)
{
    realm.Refresh();
    count++;
    Thread.Sleep(2000);
    Console.WriteLine($"----------------{realm.SyncSession.ConnectionState}-------------------");
    var myCars = realm.All<MotorRent>();//.Where(car => car.Avalible);
    Console.WriteLine($"count:{count}");
    Console.WriteLine($"***{myCars.Count()}****");
    // Console.WriteLine($"---{myCars.FirstOrDefault().CarNo}---{myCars.FirstOrDefault().Available}");
    // foreach (var car in myCars)
    // {
    //     Console.WriteLine($"Completed car: {car.CarNo}");
    // }
}

// int count = 1;
// while (true)
// {
//     Thread.Sleep(2000);
//     if (count % 2 != 0)
//     {
//         var testItem = new Car
//         {
//             CarNo = "Bensss",
//             Available = 0
//         };
//         await realm.WriteAsync(() =>
//         {
//             realm.Add(testItem);
//         });
//     }
//     else
//     {
//         var mostExpensiveGuitar = realm.All<Car>().Where(x => x.CarNo == "Bensss").FirstOrDefault();
//         await realm.WriteAsync(() =>
//         {
//             realm.Remove(mostExpensiveGuitar);
//         });
//     }
//     count++;
//     var myCars = realm.All<Car>();
//     Console.WriteLine($"***{myCars.Count()}****");
// }
// Console.ReadLine();
#endregion




// // 開啟 client端 realm
// var realm = Realm.GetInstance();
// // 查詢資料
// var allCars = realm.All<Car>();
// Console.WriteLine(allCars.Count());




#region 測試二
// try
// {
//     // Init會產生 app實例，並設定 serviceInitialised = true
//     await RealmService.Init();
//     var config = new RealmConfiguration();
//     Realm.DeleteRealm(config);
//     // await RealmService.RegisterAsync("mongo229@zzz.com", "hims99878");

//     // Login會執行 GetRealm
//     await RealmService.LoginAsync();
//     // await RealmService.LogoutAsync();
//     // 將 mainThreadRealm賦值，但程式只有登出用到 mainThreadRealm，感覺沒啥用? 而且還會在執行一次 GetRealm，感覺就很奇怪
//     // 我修改Login會回傳物件，這步就可以省略
//     // Realm realm = RealmService.GetMainThreadRealm();
//     // Console.WriteLine($"洽巴巴 3---{RealmService.CurrentUser}");

//     // SubscriptionType a = RealmService.GetCurrentSubscriptionType(realm);
//     // Console.WriteLine($"洽巴巴 4-----{a}");

//     // while (true)
//     // {
//     //     Thread.Sleep(3000);
//     //     Console.WriteLine($"-----------------------------------");
//     //     var completedItems = realm.All<Item>();//.Where(item => item.Avalible);
//     //     foreach (var item in completedItems)
//     //     {
//     //         Console.WriteLine($"Completed item: {item.Summary}");
//     //     }
//     // }
// }
// catch (Exception e)
// {
//     Console.WriteLine(e);
//     await RealmService.LogoutAsync();
// }
#endregion