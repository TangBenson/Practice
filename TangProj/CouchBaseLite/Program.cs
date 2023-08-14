using System;
using Couchbase.Lite;
using Couchbase.Lite.Query;
using Couchbase.Lite.Sync;

// Get the database (and create it if it doesn't exist)
var database = new Database("mydb");

#region client針對couchbaselite做操作

// Create a new document (i.e. a record) in the database
// string? id = null;
// using (var mutableDoc = new MutableDocument())
// {
//     // mutableDoc.SetFloat("version", 2.0f)
//     //     .SetString("type", "SDK");
//     mutableDoc.SetString("version", "1.0.0")
//         .SetString("type", ".Net SDK");

//     // Save it to the database
//     database.Save(mutableDoc);
//     id = mutableDoc.Id;
// }

// 一次新增多筆
// database.InBatch(() =>
// {
//     for (var i = 0; i < 3; i++) {
//         using (var doc = new MutableDocument()) {
//             doc.SetString("game", $"FF{i}");
//             doc.SetString("price", $"${i},000");
//             doc.SetString("admin", "Square Enix");
//             database.Save(doc);
//             Console.WriteLine($"Saved user document {doc.GetString("name")}");
//         }
//     }
// });

// Update a document
// using (var doc = database.GetDocument(id))
// using (var mutableDoc = doc.ToMutable())
// {
//     mutableDoc.SetString("language", "C#");
//     database.Save(mutableDoc);

//     using (var docAgain = database.GetDocument(id))
//     {
//         Console.WriteLine($"Document ID :: {docAgain.Id}");
//         Console.WriteLine($"Learning {docAgain.GetString("language")}");
//     }
// }

// Create a query to fetch documents of type SDK
// i.e. SELECT * FROM database WHERE type = "SDK"
// using (var query = QueryBuilder.Select(SelectResult.All())
//     .From(DataSource.Database(database)))
// // .Where(Expression.Property("type").EqualTo(Expression.String("aa"))))
// {
//     // Run the query
//     var result = query.Execute();
//     foreach (var q in result)
//     {
//         Console.WriteLine(q.GetDictionary(0).ToJSON());
//     }
//     Console.WriteLine($"柯基 ~ 水上飛基");
//     var result2 = query.Execute();
//     Console.WriteLine($"Number of rows :: {result2.AllResults().Count}");
//     Console.WriteLine($"Data Type :: {result2.GetType()}");
// }
# endregion

#region 這段在幹嘛不知道
/*
// Create replicator to push and pull changes to and from the cloud
var targetEndpoint = new URLEndpoint(new Uri("ws://localhost:4984/sgwtangdb"));
var replConfig = new ReplicatorConfiguration(database, targetEndpoint);

// Add authentication
replConfig.Authenticator = new BasicAuthenticator("sgwuser1", "pwdagain");

// Create replicator (make sure to add an instance or static variable named _Replicator)
var _Replicator = new Replicator(replConfig);
_Replicator.AddChangeListener((sender, args) =>
{
    if (args.Status.Error != null)
    {
        Console.WriteLine($"Error :: {args.Status.Error}");
    }
});

_Replicator.Start();

// Later, stop and dispose the replicator *before* closing/disposing the
*/
# endregion

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

#region 這段是去sync gateway把資料抓下來，但並不是sync gateway主動更新資料過來，和我要的不一樣!!

// var url = new Uri("ws://localhost:4985/sgwtangdb");
// var target = new URLEndpoint(url);
// var config = new ReplicatorConfiguration(database, target)
// {
//     ReplicatorType = ReplicatorType.Pull,
//     Authenticator = new BasicAuthenticator("sync_gateway", "hi1234")
// };

// var replicator = new Replicator(config);
// replicator.Start();

// replicator.AddChangeListener((sender, args) =>
// {
//     if (args.Status.Activity == ReplicatorActivityLevel.Stopped) {
//         Console.WriteLine("Replication stopped");
//     }
//     else if (args.Status.Activity == ReplicatorActivityLevel.Offline) {
//         Console.WriteLine("Replication Offline");
//     }
//     else if (args.Status.Activity == ReplicatorActivityLevel.Connecting) {
//         Console.WriteLine("Replication Connecting");
//     }
//     else if (args.Status.Activity == ReplicatorActivityLevel.Idle) {
//         Console.WriteLine("Replication Idle");
//     }
//     else if (args.Status.Activity == ReplicatorActivityLevel.Busy) {
//         Console.WriteLine("Replication Busy");
//     }
//     else {
//         Console.WriteLine("None");
//     }
// });
// replicator.AddChangeListener((sender, args) =>
// {
//     if (args.Status.Error != null) {
//         Console.WriteLine($"Error :: {args.Status.Error}");
//     }
// });

// for (int e = 0; e < 10000; e++)
// {
//     using (var query = QueryBuilder.Select(SelectResult.All()).From(DataSource.Database(database)))
//     {
//         var result2 = query.Execute();
//         Console.WriteLine($"Number of rows :: {result2.AllResults().Count}");
//     }
//     Thread.Sleep(3000);
// }
# endregion

#region 與Server即時同步
// initialize the replicator configuration
var url = new Uri("ws://localhost:4985/sgwtangdb");
var thisUrl = new URLEndpoint(url);
var config = new ReplicatorConfiguration(database, thisUrl)
{
    // Set replicator type
    ReplicatorType = ReplicatorType.PushAndPull,

    // Set autopurge option
    // here we override its default
    EnableAutoPurge = false,

    // Configure Sync Mode
    Continuous = true, // default value

    // Configure Server Security -- only accept self-signed certs
    AcceptOnlySelfSignedServerCertificate = true,

    // Configure Client Security 
    // Configure basic auth using user credentials
    Authenticator = new BasicAuthenticator("sync_gateway", "hi1234"),
};
// Initialize and start a replicator
// Initialize replicator with configuration data
var replicator = new Replicator(config);

Benson a = new Benson();
a.Methosd(replicator, database);
public class Benson
{
    private static ListenerToken _thisListenerToken;
    public void Methosd(Replicator replicator, Database db)
    {
        _thisListenerToken = replicator.AddChangeListener((sender, args) =>
        {
            if (args.Status.Activity == ReplicatorActivityLevel.Stopped)
            {
                Console.WriteLine("Replication stopped");
            }
        });

        // Start replicator
        replicator.Start();
        for (int e = 11; e < 14; e++)
        {
            using (var query = QueryBuilder.Select(SelectResult.All()).From(DataSource.Database(db)))
            {
                var result2 = query.Execute();
                // Console.SetCursorPosition(0, 10);
                Console.WriteLine($"Number of rows :: {result2.AllResults().Count}");
                
                string? id = null;
                using (var mutableDoc = new MutableDocument())
                {
                    mutableDoc.SetString("version", $"{e}.0.1")
                        .SetString("type", "ww");

                    // Save it to the database
                    db.Save(mutableDoc);
                    id = mutableDoc.Id;
                }
                using (var doc = db.GetDocument(id))
                using (var mutableDoc = doc.ToMutable())
                {
                    mutableDoc.SetString("language", "C#");
                    db.Save(mutableDoc);

                    using (var docAgain = db.GetDocument(id))
                    {
                        Console.WriteLine($"Document ID :: {docAgain.Id}");
                        Console.WriteLine($"Learning {docAgain.GetString("language")}");
                    }
                }
            }
            Thread.Sleep(1000);
        }
    }
};
#endregion