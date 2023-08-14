// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class Yield_Return
//     {
//         static void Main(string[] args)
//         {
//             Test2();
//         }

//         private static void Test()
//         {
//             var stTime = DateTime.Now;
//             Log("Dump All Start");
//             // 用 foreach 列出所有結果
//             foreach (var file in FilterRandomDataTest("C:\\Users\\benson922\\Documents\\Github_Mine\\TangProj\\CsharpStudy\\content\\testyield"))
//             {
//                 Log(file);
//             }
//             Log("End");

//             stTime = DateTime.Now;
//             Log("Show First 3 Start");
//             // 只讀前三筆，這次用 LINQ 寫
//             FilterRandomDataTest(@"C:\Users\benson922\Documents\Github_Mine\TangProj\CsharpStudy\content\testyield").Take(3).ToList().ForEach(file => Log(file));
//             Log("End");

//             // Console.ReadLine();

//             void Log(string message)
//             {
//                 Console.WriteLine($"{(DateTime.Now - stTime).TotalMilliseconds / 1000:000.00}s {message}");
//             }

//             // string[] FilterRandomDataTest(string path)
//             // {
//             //     return Directory.GetFiles(path)
//             //         .Where(o => File.ReadAllText(o).TrimEnd(Environment.NewLine.ToCharArray()).EndsWith("00"))
//             //         .ToArray();
//             // }

//             // 使用 yield不用等待所有跑完才顯示。而是有符合的就立刻顯示
//             IEnumerable<string> FilterRandomDataTest(string path)
//             {
//                 foreach (var file in Directory.GetFiles(path))
//                 {
//                     if (File.ReadAllText(file)
//                             .TrimEnd(Environment.NewLine.ToCharArray()).EndsWith("000"))
//                         yield return file;
//                 }
//             }
//         }
//         private static void Test2()
//         {
//             /*
//             使用 LINQ的 Enumerable.Range方法生成一個範圍從0到99999的整數序列，並使用Select方法將每個整數映射為一個新的Guid對象，
//             最後使用ToList方法將結果轉換為List<Guid>。Guid是一種全局唯一識別符，通常用於在應用程式中生成唯一的標識符或標記資料
//             */
//             var guidPool = Enumerable.Range(0, 100000).Select(i => Guid.NewGuid()).ToList();
//             ShowMemSize();
//             Console.WriteLine(GetGuidStrings().Count());
//             ShowMemSize();
//             Console.WriteLine(GetGuidStrings().First());
//             ShowMemSize();
//             // Console.ReadLine();

//             // string[] GetGuidStrings()
//             // {
//             //     return guidPool.Select(o => o.ToString()).ToArray();
//             // }
//             IEnumerable<string> GetGuidStrings()
//             {
//                 foreach (var guid in guidPool)
//                     yield return guid.ToString();
//             }

//             void ShowMemSize()
//             {
//                 // 當前應用程式的總內存使用量
//                 var memSz = GC.GetTotalMemory(false) / 1024 / 1024;
//                 Console.WriteLine($"Mem Size = {memSz}MB");
//             }
//         }
//     }
// }