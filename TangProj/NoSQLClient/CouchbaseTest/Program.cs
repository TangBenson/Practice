using System.Threading.Tasks;
using Couchbase;

var cluster = await Cluster.ConnectAsync(
    // Update these credentials for your Local Couchbase instance!
    "couchbase://localhost",
    "Admintang",
    "123456");

// get a bucket reference
var bucket = await cluster.BucketAsync("motorparking"); //travel-sample    get-started-bucketTang


// get a user-defined collection reference
var scope = await bucket.ScopeAsync("_default"); //tenant_agent_00
var collection = await scope.CollectionAsync("_default"); //bookings

// Upsert Document
var upsertResult = await collection.UpsertAsync("ID80345", new { Date = "19890922", Weather = "Sun", type = "SDK" });
var getResult = await collection.GetAsync("ID80345");

Console.WriteLine(getResult.ContentAs<dynamic>());



// // Call the QueryAsync() function on the scope object and store the result.
// var inventoryScope = bucket.Scope("inventory");
// var queryResult = await inventoryScope.QueryAsync<dynamic>("SELECT * FROM airline WHERE id = 10");

// // Iterate over the rows to access result data and print to the terminal.
// await foreach (var row in queryResult) {
//     Console.WriteLine(row);
// }