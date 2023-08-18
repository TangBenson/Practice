using Realms;
using MongoDB.Bson;

namespace MongodbRealm
{
    public class Friend : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
    }
}