using System.Text.Json;
using Realms;
using Realms.Sync;
using MongodbRealmSync.Models;

namespace MongodbRealmSync;

public static class RealmService
{
    private static bool serviceInitialised;
    private static Realms.Sync.App app;
    private static Realm mainThreadRealm;
    /*
    C# 8 中的一種簡寫語法，允許在一個類中聲明一個只讀屬性。這個屬性的值是透過表達式計算得出
    當你訪問 CurrentUser 屬性時，它實際上會調用 app.CurrentUser 並將其值返回給你，
    就像訪問一個常規屬性一樣，但它實際上是通過表達式計算得出的。這種語法可以讓你更方便地訪問計算屬性值，同時保持只讀的性質
    */
    public static User CurrentUser => app.CurrentUser;



    //初始化 Realm 服務
    public static async Task Init()
    {
        //如果服務已初始化，則直接返回
        if (serviceInitialised)
        {
            return;
        }
        //使用 StreamReader 讀取文件內容
        // using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("atlasConfig.json");
        string filePath = @"C:\Users\benso\OneDrive\文件\dotnetProject\Practice\TangProj\NoSQLClient\MongodbRealmSync\atlasConfig.json";
        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new(fileStream);
        var fileContent = await reader.ReadToEndAsync();
        //將文件內容反序列化為 RealmAppConfig 對象
        var config = JsonSerializer.Deserialize<RealmAppConfig>(fileContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Console.WriteLine(config.AppId);

        //設置了 Realm 應用程序的配置
        var appConfiguration = new AppConfiguration(config.AppId)
        {
            BaseUri = new Uri(config.BaseUrl)
        };
        //創建了一個 Realm 應用程序實例
        app = Realms.Sync.App.Create(appConfiguration);

        serviceInitialised = true;
    }






    //返回主線程的 Realm 實例，如果不存在則創建一個
    public static Realm GetMainThreadRealm()
    {
        //...??= 運算子。這個運算子的作用是在變數為 null 時，將右邊的值賦值給左邊的變數
        return mainThreadRealm ??= GetRealm();
    }
    //返回一個 Realm 實例，同時配置了數據同步設置
    public static Realm GetRealm()
    {
        var config = new FlexibleSyncConfiguration(app.CurrentUser)
        {
            PopulateInitialSubscriptions = (realm) =>
            {
                var (query, queryName) = GetQueryForSubscriptionType(realm, SubscriptionType.Mine);
                realm.Subscriptions.Add(query, new SubscriptionOptions { Name = queryName });
            }
        };
        return Realm.GetInstance(config);
    }






    //公共的靜態異步方法，用於註冊新用戶
    public static async Task RegisterAsync(string email, string password)
    {
        await app.EmailPasswordAuth.RegisterUserAsync(email, password);
    }
    public static async Task LoginAsync(string email, string password)
    {
        await app.LogInAsync(Credentials.EmailPassword(email, password));
        //This will populate the initial set of subscriptions the first time the realm is opened

        // 打印当前线程的 ID
        Console.WriteLine($"1-Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
        
        using var realm = GetRealm();
        //用戶登錄後，等待數據同步完成
        // await realm.Subscriptions.WaitForSynchronizationAsync();
        realm.Subscriptions.WaitForSynchronizationAsync();

        Console.WriteLine($"2-Current thread ID: {Thread.CurrentThread.ManagedThreadId}");

        while (true)
        {
            Thread.Sleep(3000);
            Console.WriteLine($"-----------------------------------");
            var completedItems = realm.All<Item>();//.Where(item => item.Avalible);
            foreach (var item in completedItems)
            {
                Console.WriteLine($"Completed item: {item.Summary}");
            }
        }
    }
    public static async Task LogoutAsync()
    {
        await app.CurrentUser.LogOutAsync();
        mainThreadRealm?.Dispose();
        mainThreadRealm = null;
    }







    //設置訂閱的類型
    public static async Task SetSubscription(Realm realm, SubscriptionType subType)
    {
        if (GetCurrentSubscriptionType(realm) == subType)
        {
            return;
        }

        realm.Subscriptions.Update(() =>
        {
            realm.Subscriptions.RemoveAll(true);

            var (query, queryName) = GetQueryForSubscriptionType(realm, subType);

            realm.Subscriptions.Add(query, new SubscriptionOptions { Name = queryName });
        });

        //There is no need to wait for synchronization if we are disconnected
        if (realm.SyncSession.ConnectionState != ConnectionState.Disconnected)
        {
            await realm.Subscriptions.WaitForSynchronizationAsync();
        }
    }
    //根據訂閱類型返回相應的查詢和查詢名稱
    public static SubscriptionType GetCurrentSubscriptionType(Realm realm)
    {
        var activeSubscription = realm.Subscriptions.FirstOrDefault();

        return activeSubscription.Name switch
        {
            "all" => SubscriptionType.All,
            "mine" => SubscriptionType.Mine,
            _ => throw new InvalidOperationException("Unknown subscription type")
        };
    }
    private static (IQueryable<Item> Query, string Name) GetQueryForSubscriptionType(Realm realm, SubscriptionType subType)
    {
        IQueryable<Item> query = null;
        string queryName = null;

        if (subType == SubscriptionType.Mine)
        {
            query = realm.All<Item>().Where(i => i.OwnerId == CurrentUser.Id);
            queryName = "mine";
        }
        else if (subType == SubscriptionType.All)
        {
            query = realm.All<Item>();
            queryName = "all";
        }
        else
        {
            throw new ArgumentException("Unknown subscription type");
        }

        return (query, queryName);
    }
}

public enum SubscriptionType
{
    Mine,
    All,
}

public class RealmAppConfig
{
    public string AppId { get; set; }

    public string BaseUrl { get; set; }
}


