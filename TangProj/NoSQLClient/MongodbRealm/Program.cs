using MongodbRealm;
using Realms;

// 開啟 client端 realm
var realm = Realm.GetInstance();

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
var testItem = new Item
{
    Name = "Do this thing",
    Status = "A",
    Partition = "Aimee"
};
await realm.WriteAsync(() =>
{
    realm.Add(testItem);
});
// 寫入資料
var testItem2 =
    await realm.WriteAsync(() =>
    {
        return realm.Add<Item>(new Item
        {
            Name = "Do this thing, too",
            Status = "A",
            Partition = "Satya"
        });
    }
);

// 查詢資料
var allFriends = realm.All<Friend>();
Console.WriteLine(allFriends.Count());
Console.WriteLine(allFriends);
// Console.WriteLine(allFriends.Where(x => x.Name == "Adam"));

// 刪除資料
// var mostExpensiveGuitar = realm.All<Friend>().OrderByDescending(g => g.Age).FirstOrDefault();
// realm.Write(() =>
// {
//     realm.Remove(mostExpensiveGuitar);
// });
