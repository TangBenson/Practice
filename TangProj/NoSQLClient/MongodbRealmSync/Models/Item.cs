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
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; }//= ObjectId.GenerateNewId();
        public string CarNo { get; set; }
        [MapTo("available")]
        public int Available { get; set; }

        // public bool IsMine => OwnerId == RealmService.CurrentUser.Id;
    }
}