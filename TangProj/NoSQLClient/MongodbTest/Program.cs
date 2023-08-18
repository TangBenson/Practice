using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

#region Mongo官方連線寫法
const string connectionUri = "mongodb+srv://benson922:hi20230524@cluster0.vcgxcov.mongodb.net/?retryWrites=true&w=majority";
var settings = MongoClientSettings.FromConnectionString(connectionUri);
// Set the ServerApi field of the settings object to Stable API version 1
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
// Create a new client and connect to the server
var client = new MongoClient(settings);
// Send a ping to confirm a successful connection
try
{
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

// Access a database
var database = client.GetDatabase("benson_test"); //IMongoDatabase database = client.GetDatabase("test");
# endregion


# region 查詢db清單
// var databaseList = client.ListDatabases().ToList();
// foreach (var db in databaseList)
// {
//     Console.WriteLine(db["name"]);
// }
# endregion


# region 查詢db下的collection清單
// var collectionList = database.ListCollectionNames().ToList();
// foreach (var collectionName in collectionList)
// {
//     Console.WriteLine(collectionName);
// }
# endregion


# region Insert a document into a collection
// var collection = database.GetCollection<BsonDocument>("Friend");
// var document = new BsonDocument
// {
//     { "name", "Dante" },
//     { "age", "592" },
//     { "address", "Devil May Cry" }
//     // { "A", "1" },
//     // { "B", "2" },
//     // { "C", "3" }
// };
// collection.InsertOne(document);
# endregion


# region Read from a collection
// var collection = database.GetCollection<BsonDocument>("benson_test");
// var filter = new BsonDocument(); //()內若不傳值則會找出所有 document
// // var filter = new BsonDocument("age","33");
// using (var cursor = await collection.FindAsync(filter))
// {
//     while (await cursor.MoveNextAsync())
//     {
//         var batch = cursor.Current;
//         foreach (var document in batch)
//         {
//             Console.WriteLine(document);
//         }
//     }
// }
// Console.WriteLine("-------------------");
// var list = collection.AsQueryable<BsonDocument>().Where(x => x["name"] == "Peggy").ToList();
// foreach (var doc in list)
// {
//     Console.WriteLine(doc);
// }
# endregion


# region 地理位置查詢
var collection = database.GetCollection<BsonDocument>("loglist");

// 建立 2dsphere index
// var indexModel = new CreateIndexModel<BsonDocument>(
//     Builders<BsonDocument>.IndexKeys.Geo2DSphere("Location"),
//     new CreateIndexOptions { Name = "LocationIndex" }
// );
// await collection.Indexes.CreateOneAsync(indexModel);

Console.WriteLine("開始~~");
List<double> times = new List<double>();
for (int i = 0; i < 20; i++)
{
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    var radius = 0.5 / 6731.0;
    var filter = Builders<BsonDocument>.Filter.GeoWithinCenterSphere(
        "Location.coordinates",
        121.5347344292775, 25.048803720610916,
        radius
    );

    var gpsresult = await collection.Find(filter).ToListAsync();
    stopwatch.Stop();
    TimeSpan elapsed = stopwatch.Elapsed;
    double milliseconds = elapsed.TotalMilliseconds;
    Console.WriteLine($"耗時：{milliseconds}");
    times.Add(milliseconds);
}
Console.WriteLine($"avg time：{times.Average()}");

// Stopwatch stopwatch = new Stopwatch();
// stopwatch.Start();

// var radius = 0.5 / 6731.0; // 尋找半徑為 500公尺的圓形範圍內
// var filter = Builders<BsonDocument>.Filter.GeoWithinCenterSphere(
//     "Location",
//     121.5347344292775, 25.048803720610916,
//     radius
// );

// var gpsresult = await collection.Find(filter).ToListAsync();
// stopwatch.Stop();
// TimeSpan elapsed = stopwatch.Elapsed;
// double milliseconds = elapsed.TotalMilliseconds;
// Console.WriteLine($"耗時：{milliseconds}");
// Console.WriteLine($"物件數：{gpsresult.Count}");
// Console.WriteLine($"物件數：{gpsresult.Count()}"); //LINQ擴展方法，可以帶入條件
// var locationList = gpsresult.Select(document => document["Location"]).ToList();
// foreach (var gps in locationList)
// {
//     Console.WriteLine(gps["coordinates"]);
// }
// foreach (var cars in gpsresult)
// {
//     Console.WriteLine(cars["CarNo"]);
// }
#endregion