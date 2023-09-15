public sealed class SingletonPatternLazyT
{
    private static readonly Lazy<SingletonPatternLazyT> lazy = new Lazy<SingletonPatternLazyT>(() => new SingletonPatternLazyT());

    // 私有且無參數構造函式，防止直接實例化。但本範例因main在同一個class檔，所以還是能實例化
    private SingletonPatternLazyT()
    {
    }

    // public static SingletonPatternLazyT Instance { get { return lazy.Value; } }
    public static SingletonPatternLazyT Instance => lazy.Value;

    static void Main()
    {
        Console.WriteLine("Lazy");
        SingletonPatternLazyT instance1 = SingletonPatternLazyT.Instance; //只有在第一次訪問時才會初始化。這是經典的單例實現方式
        SingletonPatternLazyT instance2 = SingletonPatternLazyT.Instance;
        SingletonPatternLazyT a1 = new(); //不考慮是否已經存在一個實例。每次使用 new 關鍵字時都會創建一個新的對象，違反了單例模式的設計目標
        SingletonPatternLazyT a2 = new();
        Console.WriteLine(instance1 == instance2); // 输出：True，两个实例相同
        Console.WriteLine(a1 == a2); // False
    }
}