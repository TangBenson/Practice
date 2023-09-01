// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MongoDB.Bson;
// using Realms;

// namespace MongodbRealmSync.Models
// {
//     public partial class Car : IRealmObject
//     {
//         [MapTo("_id")]
//         [PrimaryKey]
//         public ObjectId Id { get; set; }
//         public string CarNo { get; set; }
//         [MapTo("available")]
//         public int Available { get; set; }
//     }
    
// }


using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

namespace MongodbRealmSync.Models;
public partial class MotorRent : IRealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    public string CarNo { get; set; }

    public int? GiveMin { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    [MapTo("device3TBA")]
    public double Device3TBA { get; set; }
}
