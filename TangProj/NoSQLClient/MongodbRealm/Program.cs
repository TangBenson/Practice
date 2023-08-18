using MongodbRealm;
using Realms;

var config = new RealmConfiguration();
Realm.DeleteRealm(config);

// 開啟 client端 realm
var realm = Realm.GetInstance();

// 查詢資料
var allFriends = realm.All<Friend>();
Console.WriteLine(allFriends.Count());
Console.WriteLine(allFriends);

// 寫入資料
// realm.Write(() =>
// {
//     realm.Add(new Friend()
//     {
//         Name = "Adam9",
//         Age = "4x",
//         Sex = "Male",
//         Like = "hit child"
//     });
// });
// 寫入資料
var testItem = new Friend
{
    Name = "Bensss",
    Age = "987"
};
await realm.WriteAsync(() =>
{
    realm.Add(testItem);
});

// 查詢資料
Console.WriteLine(allFriends.Count());
// Console.WriteLine(allFriends.Where(x => x.Name == "Adam"));
foreach(var i in allFriends){
    Console.WriteLine(i.Age);
    Console.WriteLine(i.Name);
}


// 刪除資料
// var mostExpensiveGuitar = realm.All<Friend>().OrderByDescending(g => g.Age).FirstOrDefault();
// realm.Write(() =>
// {
//     realm.Remove(mostExpensiveGuitar);
// });


realm.Dispose();