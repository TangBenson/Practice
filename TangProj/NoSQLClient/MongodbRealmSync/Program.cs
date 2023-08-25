using MongodbRealmSync;
using MongodbRealmSync.Models;
using Realms;
using Realms.Sync;

Console.WriteLine("---START---");

// App app = App.Create("application-0-gyubp");
// var config = new FlexibleSyncConfiguration(app.CurrentUser);
// Realm.DeleteRealm(config);

try
{
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
    await realm.Subscriptions.WaitForSynchronizationAsync();
    Console.WriteLine("Cao");
    int count = 0;
    while (true)
    {
        count++;
        Thread.Sleep(2000);
        Console.WriteLine($"-----------------------------------");
        var completedItems = realm.All<Car>();//.Where(item => item.Avalible);
        Console.WriteLine($"count次數:{count}");
        Console.WriteLine($"***{completedItems.Count()}****");
        Console.WriteLine($"---{completedItems.FirstOrDefault().CarNo}---{completedItems.FirstOrDefault().Available}");
        // foreach (var item in completedItems)
        // {
        //     Console.WriteLine($"Completed item: {item.CarNo}");
        // }
    }


    // // Init會產生 app實例，並設定 serviceInitialised = true
    // await RealmService.Init();
    // var config = new RealmConfiguration();
    // Realm.DeleteRealm(config);
    // // await RealmService.RegisterAsync("mongo229@zzz.com", "hims99878");
    // // Login會執行 GetRealm
    // await RealmService.LoginAsync();
    // await RealmService.LogoutAsync();
    // // 將 mainThreadRealm賦值，但程式只有登出用到 mainThreadRealm，感覺沒啥用? 而且還會在執行一次 GetRealm，感覺就很奇怪
    // // 我修改Login會回傳物件，這步就可以省略
    // // Realm realm = RealmService.GetMainThreadRealm();
    // // Console.WriteLine($"洽巴巴 3---{RealmService.CurrentUser}");

    // // SubscriptionType a = RealmService.GetCurrentSubscriptionType(realm);
    // // Console.WriteLine($"洽巴巴 4-----{a}");

    // // while (true)
    // // {
    // //     Thread.Sleep(3000);
    // //     Console.WriteLine($"-----------------------------------");
    // //     var completedItems = realm.All<Item>();//.Where(item => item.Avalible);
    // //     foreach (var item in completedItems)
    // //     {
    // //         Console.WriteLine($"Completed item: {item.Summary}");
    // //     }
    // // }
}
catch (Exception e)
{
    Console.WriteLine(e);
    await RealmService.LogoutAsync();
}