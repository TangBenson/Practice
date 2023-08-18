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
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("owner_id")]
        [Required]
        public string OwnerId { get; set; }

        [MapTo("summary")]
        [Required]
        public string Summary { get; set; }

        [MapTo("isComplete")]
        public bool IsComplete { get; set; }

        public bool IsMine => OwnerId == RealmService.CurrentUser.Id;



        //以下內容從官方下載時沒有，但 IRealmObject要求實作這些，我就讓 ide自動帶入
        //但我發現把專案檔的<Nullable>enable</Nullable>註解掉就不用了耶，真奇怪
        // public IRealmAccessor Accessor => throw new NotImplementedException();

        // public bool IsManaged => throw new NotImplementedException();

        // public bool IsValid => throw new NotImplementedException();

        // public bool IsFrozen => throw new NotImplementedException();

        // public Realm? Realm => throw new NotImplementedException();

        // public ObjectSchema? ObjectSchema => throw new NotImplementedException();

        // public DynamicObjectApi DynamicApi => throw new NotImplementedException();

        // public int BacklinksCount => throw new NotImplementedException();

        // public void SetManagedAccessor(IRealmAccessor accessor, IRealmObjectHelper? helper = null, bool update = false, bool skipDefaults = false)
        // {
        //     throw new NotImplementedException();
        // }
    }
}