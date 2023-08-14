using System;
using Couchbase.Lite;
using Couchbase.Lite.Query;
using Couchbase.Lite.Sync;

// Get the database (and create it if it doesn't exist)
var database = new Database("mydb");

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
var thisReplicator = new Replicator(config);

Benson a = new Benson();
a.Methosd(thisReplicator, database);
public class Benson
{
    private static ListenerToken _thisListenerToken;
    public void Methosd(Replicator thisReplicator, Database db)
    {
        _thisListenerToken = thisReplicator.AddChangeListener((sender, args) =>
        {
            if (args.Status.Activity == ReplicatorActivityLevel.Stopped)
            {
                Console.WriteLine("Replication stopped");
            }
        });

        // Start replicator
        thisReplicator.Start();
        for (int e = 0; e < 10000; e++)
        {
            using (var query = QueryBuilder.Select(SelectResult.All()).From(DataSource.Database(db)))
            {
                var result2 = query.Execute();
                // Console.SetCursorPosition(0,10);
                Console.WriteLine($"Number of rows :: {result2.AllResults().Count}");
            }
            Thread.Sleep(1000);
        }
    }
};