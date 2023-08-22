using MongodbRealmSync;
using MongodbRealmSync.Models;
using Realms;
using Realms.Sync;

Console.WriteLine("---START---");
// Console.WriteLine($"洽巴巴 1---{RealmService.CurrentUser}");

// Init會產生 app實例，並設定 serviceInitialised = true
await RealmService.Init();
// Console.WriteLine($"洽巴巴 2---{RealmService.CurrentUser}");
// Console.WriteLine($"洽巴巴 2---{RealmService.CurrentUser.Id}");


try
{
    // App app = App.Create("application-0-ofpgd");
    // var user = await app.LogInAsync(Credentials.Anonymous());
    // var config = new FlexibleSyncConfiguration(app.CurrentUser)
    // {
    //     PopulateInitialSubscriptions = (realm) =>
    //     {
    //         var myItems = realm.All<Item>();
    //         realm.Subscriptions.Add(myItems);
    //     }
    // };
    // var realm = await Realm.GetInstanceAsync(config);
    // realm.Subscriptions.WaitForSynchronizationAsync();
    // int count = 0;
    // while (count < 25)
    // {
    //     count++;
    //     Thread.Sleep(3000);
    //     Console.WriteLine($"-----------------------------------");
    //     var completedItems = realm.All<Item>();//.Where(item => item.Avalible);
    //     Console.WriteLine($"***{completedItems.Count()}****");
    //     foreach (var item in completedItems)
    //     {
    //         Console.WriteLine($"Completed item: {item.Summary}");
    //     }
    // }


    var config = new RealmConfiguration();
    Realm.DeleteRealm(config);
    // await RealmService.RegisterAsync("mongo229@zzz.com", "hims99878");
    // Login會執行 GetRealm
    await RealmService.LoginAsync("mongo229@zzz.com", "hims99878");
    await RealmService.LogoutAsync();
    // 將 mainThreadRealm賦值，但程式只有登出用到 mainThreadRealm，感覺沒啥用? 而且還會在執行一次 GetRealm，感覺就很奇怪
    // 我修改Login會回傳物件，這步就可以省略
    // Realm realm = RealmService.GetMainThreadRealm();
    // Console.WriteLine($"洽巴巴 3---{RealmService.CurrentUser}");

    // SubscriptionType a = RealmService.GetCurrentSubscriptionType(realm);
    // Console.WriteLine($"洽巴巴 4-----{a}");

    // while (true)
    // {
    //     Thread.Sleep(3000);
    //     Console.WriteLine($"-----------------------------------");
    //     var completedItems = realm.All<Item>();//.Where(item => item.Avalible);
    //     foreach (var item in completedItems)
    //     {
    //         Console.WriteLine($"Completed item: {item.Summary}");
    //     }
    // }
}
catch (Exception e)
{
    Console.WriteLine(e);
    await RealmService.LogoutAsync();
}