// // 這是個不好的案例，此方式不是執行緒安全，無法確保在多執行緒情況下是唯一的實例
// public sealed class NotThreadSafeSingleton
// {
//     private static NotThreadSafeSingleton _instance = null;

//     // 私有且無參數構造函式，防止直接實例化。但本範例因main在同一個class檔，所以還是能實例化
//     private NotThreadSafeSingleton()
//     {
//     }

//     // 提供全域訪問點
//     public static NotThreadSafeSingleton Instance
//     {
//         get
//         {
//             _instance ??= new NotThreadSafeSingleton();

//             return _instance;
//         }
//     }

//     static void Main()
//     {
//         NotThreadSafeSingleton instance1 = NotThreadSafeSingleton.Instance; //只有在第一次訪問時才會初始化。這是經典的單例實現方式
//         NotThreadSafeSingleton instance2 = NotThreadSafeSingleton.Instance;
//         NotThreadSafeSingleton a1 = new(); //不考慮是否已經存在一個實例。每次使用 new 關鍵字時都會創建一個新的對象，違反了單例模式的設計目標
//         NotThreadSafeSingleton a2 = new();
//         Console.WriteLine(instance1 == instance2); // 输出：True，两个实例相同
//         Console.WriteLine(a1 == a2); // False
//     }
// }
