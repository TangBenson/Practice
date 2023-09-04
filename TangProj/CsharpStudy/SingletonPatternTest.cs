// public sealed class SingletonPatternTest
// {
//     // 使用 Lazy<T> 來實現延遲初始化
//     private static readonly Lazy<SingletonPatternTest> lazyInstance = new(() => new SingletonPatternTest());

//     // 提供全域訪問點
//     public static SingletonPatternTest Instance
//     {
//         get { return lazyInstance.Value; }
//     }

//     // 私有構造函式，防止直接實例化。但本範例因main在同一個class檔，所以還是能實例化
//     private SingletonPatternTest(){}

//     // 其他成員方法和屬性
//     public void SayHello()
//     {
//         Console.WriteLine("Hello from Singleton!");
//     }

//     static void Main()
//     {
//         SingletonPatternTest instance1 = SingletonPatternTest.Instance; //只有在第一次訪問時才會初始化。這是經典的單例實現方式
//         SingletonPatternTest instance2 = SingletonPatternTest.Instance;
//         SingletonPatternTest a1 = new(); //不考慮是否已經存在一個實例。每次使用 new 關鍵字時都會創建一個新的對象，違反了單例模式的設計目標
//         SingletonPatternTest a2 = new();
//         Console.WriteLine(instance1 == instance2); // 输出：True，两个实例相同
//         Console.WriteLine(a1 == a2); // False
//         instance1.SayHello();
//         a1.SayHello();
//     }
// }
