// //使用 Lock 來確保執行緒安全
// public sealed class SingletonPatternThreadLock
// {
//     private static readonly object padlock = new object();

//     private static SingletonPatternThreadLock _instance = null;

//     private SingletonPatternThreadLock()
//     {
//     }

//     public static SingletonPatternThreadLock Instance
//     {
//         get
//         {
//             lock (padlock)
//             {
//                 if (_instance == null)
//                 {
//                     _instance = new SingletonPatternThreadLock();
//                 }
//                 return _instance;
//             }
//         }
//     }
//     static void Main()
//     {
//         Console.WriteLine("ThreadLock");
//         SingletonPatternThreadLock instance1 = SingletonPatternThreadLock.Instance; //只有在第一次訪問時才會初始化。這是經典的單例實現方式
//         SingletonPatternThreadLock instance2 = SingletonPatternThreadLock.Instance;
//         SingletonPatternThreadLock a1 = new(); //不考慮是否已經存在一個實例。每次使用 new 關鍵字時都會創建一個新的對象，違反了單例模式的設計目標
//         SingletonPatternThreadLock a2 = new();
//         Console.WriteLine(instance1 == instance2); // 输出：True，两个实例相同
//         Console.WriteLine(a1 == a2); // False
//     }
// }