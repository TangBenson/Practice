using Realms;

namespace MongodbRealm
{
    public class Friend : RealmObject
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Sex { get; set; }
        public string? Like { get; set; }
    }
}