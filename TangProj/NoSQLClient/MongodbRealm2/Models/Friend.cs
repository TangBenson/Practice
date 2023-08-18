using Realms;

namespace MongodbRealm1
{
    public class Friend : RealmObject
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public string? Like { get; set; }
    }
}