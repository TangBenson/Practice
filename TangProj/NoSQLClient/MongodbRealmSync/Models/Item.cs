using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Realms;
using Realms.Schema;
using Realms.Weaving;

namespace MongodbRealmSync.Models
{
    //實現了 IRealmObject 介面，代表是可以存儲在 Realm 數據庫中的物件
    public partial class Item : IRealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } //= ObjectId.GenerateNewId();

        [MapTo("owner_id")]
        public string OwnerId { get; set; }

        [MapTo("summary")]
        public string Summary { get; set; }

        // [MapTo("isComplete")]
        // public bool IsComplete { get; set; }

        // public bool IsMine => OwnerId == RealmService.CurrentUser.Id;
    }
}