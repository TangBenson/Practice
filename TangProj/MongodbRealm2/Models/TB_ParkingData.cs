using MongoDB.Bson;
using Realms;

namespace MongodbRealm2
{
    public class TB_ParkingData : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();

        // [MapTo("_partition")]
        // [Required]
        // public string Partition { get; set; }
        public string? name { get; set; }
        public string? age { get; set; }
        public string? like { get; set; }
    }
}