using MongodbRealmSync;
using MongodbRealmSync.Models;
using Realms;
using Realms.Sync;

Console.WriteLine("---START---");

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
var config = new FlexibleSyncConfiguration(app.CurrentUser);
//刪除本地 Realm 數據庫
// Realm.DeleteRealm(config);
//創建了一個新的 Realm 實例
var realm = await Realm.GetInstanceAsync(config);
//向 Realm 添加了一個訂閱
realm.Subscriptions.Update(() =>
{
    var myCars = realm.All<Car>();//.Where(t => t.Name == "Clifford" && t.Age > 5);
    realm.Subscriptions.Add(myCars);
});
Console.WriteLine("Peggy");
await realm.Subscriptions.WaitForSynchronizationAsync(); //等待所有訂閱的同步操作完成
realm.Refresh();
Console.WriteLine("Cao");

realm.RealmChanged += (sender, e) =>
{
    Console.WriteLine("AAAAAAA");
};

// var artist = realm.All<Car>().FirstOrDefault();
// artist!.PropertyChanged += (sender, eventArgs) =>
// {
//     var changedProperty = eventArgs.PropertyName!;
//     Console.WriteLine(
//         $@"New value set for 'artist':
//         '{changedProperty}' is now {artist.GetType().GetProperty(changedProperty)!.GetValue(artist)}");
// };

realm.All<Car>()
    .SubscribeForNotifications((sender, changes) =>
    {
        Console.WriteLine("狂浪是一種態度");
        // 没有有效的更改通知
        if (changes == null)
        {
            Console.WriteLine("~~~~~~~~~~~");
            return; //中止当前函数的执行并立即退出
        }
        // Handle individual changes
        foreach (var i in changes.DeletedIndices)
        {
            Console.WriteLine("----------");
        }
        foreach (var i in changes.InsertedIndices)
        {
            Console.WriteLine("++++++++++");
        }
        foreach (var i in changes.NewModifiedIndices)
        {
            Console.WriteLine("uuuuuuuuuuuu");
        }
        if (changes.IsCleared)
        {
            Console.WriteLine("????????????");
            return;
        }
    });
// Console.ReadLine();

// Console.WriteLine($"香蕉妳個芭樂");
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

int count = 0;
while (true)
{
    count++;
    Thread.Sleep(2000);
    Console.WriteLine($"-----------------------------------");
    var myCars = realm.All<Car>();//.Where(car => car.Avalible);
    Console.WriteLine($"count次數:{count}");
    Console.WriteLine($"***{myCars.Count()}****");
    // Console.WriteLine($"---{myCars.FirstOrDefault().CarNo}---{myCars.FirstOrDefault().Available}");
    // foreach (var car in myCars)
    // {
    //     Console.WriteLine($"Completed car: {car.CarNo}");
    // }
}





// // 開啟 client端 realm
// var realm = Realm.GetInstance();
// // 查詢資料
// var allCars = realm.All<Car>();
// Console.WriteLine(allCars.Count());





// try
// {
//     // Init會產生 app實例，並設定 serviceInitialised = true
//     await RealmService.Init();
//     var config = new RealmConfiguration();
//     Realm.DeleteRealm(config);
//     // await RealmService.RegisterAsync("mongo229@zzz.com", "hims99878");
//     // Login會執行 GetRealm
//     await RealmService.LoginAsync();
//     await RealmService.LogoutAsync();
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