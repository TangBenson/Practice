public sealed class SingletonPattern
{
    private static readonly Lazy<SingletonPattern> lazyInstance = new Lazy<SingletonPattern>(() => new SingletonPattern());

    public static SingletonPattern Instance
    {
        get { return lazyInstance.Value; }
    }

    private SingletonPattern()
    {
        // 私有构造函数，防止直接实例化
    }

    public void SayHello()
    {
        Console.WriteLine("Hello from Singleton!");
    }


    static void Main()
    {
        SingletonPattern instance1 = SingletonPattern.Instance;
        SingletonPattern instance2 = SingletonPattern.Instance;

        Console.WriteLine(instance1 == instance2); // 输出：True，两个实例相同

        instance1.SayHello();
    }
}
