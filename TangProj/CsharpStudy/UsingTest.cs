// /* 
// 引入.NET程式庫,以便使用Console類別的WriteLine()方法。
// 或是引用自己寫的namespace
// */
// using System;
// using System.Collections.Generic; // 為了使用List
// using Newtonsoft.Json; // 為了使用JsonConvert
// using System.Text.RegularExpressions; // 為了使用Regex
// using System.Linq; // 為了使用ToArray()
// using System.Collections;

// /* 
// 定義命名空間 (namespace) Demo
// */
// namespace CsharpStudy;
// class UsingTest
// {
//     static void Main(string[] args)
//     {
//         //使用 using 陳述式有一個最基本的條件，就是該物件必須有實做 IDisposable 介面，才能確保在 using 的結尾數時自動執行 Dispose() 方法。
//         // 使用 using 陳述式的另一個強烈建議的條件，就是該物件被建立的語法必須寫在 using 子句中，否則物件很有可能在被釋放後還有其他物件存取的情況，進而引發例外狀況！
//         // using(int a = 5){
//         //     //.....
//         // }

//         Console.WriteLine(DateTime.Now.AddDays(1));
//         Console.WriteLine(DateTime.Now);
        
//         // using (System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Users\benson922\Downloads\test.txt"))
//         // {
//         //     string s = null;
//         //     while ((s = sr.ReadLine()) != null)
//         //     {
//         //         Console.WriteLine(s);
//         //     }
//         // }
//     }
// }