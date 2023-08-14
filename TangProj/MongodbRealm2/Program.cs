using MongodbRealm2;
using Realms;
using Realms.Sync;

var myRealmAppId = "synctest-ogdgm";
var app = App.Create(myRealmAppId);
var user = await app.LogInAsync(Credentials.Anonymous());

//OPEN A PARTITION SYNCED REALM FOR YOUR DATA MODELS
// var config = new PartitionSyncConfiguration("Clifford", user);
// var realm = await Realm.GetInstanceAsync(config);

//OPEN A FLEXIBLE SYNCED REALM FOR YOUR DATA MODELS //Atlas後台的教學
var config = new FlexibleSyncConfiguration(app.CurrentUser);
var realm = Realm.GetInstance(config);
realm.Subscriptions.Update(() =>
{
    var myDogs = realm.All<TB_ParkingData>().Where(t => 1==1);
    realm.Subscriptions.Add(myDogs);
});
await realm.Subscriptions.WaitForSynchronizationAsync();
var allFriends = realm.All<TB_ParkingData>();
while (1 != 2)
{
    Console.WriteLine(allFriends.Count());
    Thread.Sleep(2000);
}



//官網的教學
/*
要打開Realm作為同步Realm，您可以指定在打開之前是否應該下載數據。在這裡，
我們使用彈性同步配置，並指定SDK應該在打開Realm之前始終下載最新的更新。我們還使用初始訂閱來啟動Realm
*/
// var config = new FlexibleSyncConfiguration(app.CurrentUser)
// {
//     PopulateInitialSubscriptions = (realm) =>
//     {
//         var myItems = realm.All<TB_ParkingData>().Where(n => 1 == 1);
//         realm.Subscriptions.Add(myItems);
//     }
// };
// // The process will complete when all the user's items have been downloaded.
// var realm2 = await Realm.GetInstanceAsync(config); // 開啟 client端 realm
// var subscriptions = realm2.Subscriptions;

// while (1 == 1)
// {
//     var allFriends = realm2.All<TB_ParkingData>();
//     Console.WriteLine(allFriends.Count());
//     Thread.Sleep(2000);
// }