// /* 
// 引入.NET程式庫,以便使用Console類別的WriteLine()方法。
// 或是引用自己寫的namespace
// */
// using System;
// using System.Collections.Generic; // 為了使用List
// using Newtonsoft.Json; // 為了使用JsonConvert
// using System.Text.RegularExpressions; // 為了使用Regex
// using System.Linq; // 為了使用ToArray()

// /* 
// 定義命名空間 (namespace) Demo
// */
// namespace CsharpStudy;
// // C# 有 ref／out 關鍵字可以用來改變方法參數的傳遞機制，將原本的傳值（by value）改為傳址（by reference），因為有時候會碰到這樣的需求，
// // 提供給某方法的引數會希望輸出處理過的結果並回存到原本的變數上，此時就得用傳址參數 -- ref 或 out 參數來完成，兩者極為相似但有些許不同和需要注意的地方，以下摘錄自 MSDN Library：
// // 1. 以 ref 參數傳遞的引數必須先被初始化，out 則不需要。
// // 2. out 參數要在離開目前的方法之前至少有一次指派值的動作。
// class MethodParameter
// {
//     static void SampleMethod(ref int refParam, out int outParam)
//     {
//         outParam = 44;
//     }

//     static void Main(string[] args)
//     {
//         // ref 參數傳遞前必須初始化；
//         // out 參數不需初始化，但必須於方法內給值。
//         int p1 = 0;
//         int p2;

//         SampleMethod(ref p1, out p2);
//         Console.WriteLine(p1 + ", " + p2);
//     }
// }