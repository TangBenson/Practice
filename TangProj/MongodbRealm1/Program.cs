using MongodbRealm1;
using Realms;
using Realms.Sync;

var myRealmAppId = "irentappsync-aattr";
var app = App.Create(myRealmAppId);
var user = await app.LogInAsync(Credentials.Anonymous());

//OPEN A PARTITION SYNCED REALM FOR YOUR DATA MODELS
// var config = new PartitionSyncConfiguration("Clifford", user);
// var realm = await Realm.GetInstanceAsync(config);

//OPEN A FLEXIBLE SYNCED REALM FOR YOUR DATA MODELS
var config = new FlexibleSyncConfiguration(app.CurrentUser);
var realm = Realm.GetInstance(config);
realm.Subscriptions.Update(() =>
{
    var myDogs = realm.All<Dog>().Where(t => t.Name == "Hasky" && t.Age > 5 );
    realm.Subscriptions.Add(myDogs);
});
await realm.Subscriptions.WaitForSynchronizationAsync();

//USE YOUR DATA MODEL WITH YOUR SYNCED REALM
realm.Write(() =>
{
    realm.Add(new Dog { Name = "Clifford", Age = 1 });
});
var dogs = realm.All<Dog>();