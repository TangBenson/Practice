using Realms;

namespace MongodbRealm1
{
    public class Dog : RealmObject
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}