using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Realms;

namespace MongodbRealmSync.Models
{
    public partial class Car : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }
        public string CarNo { get; set; }
        [MapTo("available")]
        public int Available { get; set; }
    }
    
}