// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;

// namespace CsharpStudy;
// class IEnumerableTry
// {
//     static void Main(string[] args)
//     {
//         // 只可讀，不像List可新增
//         IEnumerable a = new List<int>() { 1, 2, 3 };
//         foreach (int i in a)
//         {
//             Console.WriteLine(i);
//         }

//         //使用 C# 中的 as 關鍵字將列表轉換為 IEnumerable
//         List<int> ilist = new List<int> { 11, 21, 31 };
//         IEnumerable<int> aa = ilist as IEnumerable<int>;
//         foreach (int i in aa)
//         {
//             Console.WriteLine(i);
//         }

//         //使用 C# 中的型別轉換方法將列表轉換為 IEnumerable
//         List<int> ilist2 = new List<int> { 1, 2, 3 };
//         IEnumerable<int> enumerable = (IEnumerable<int>)ilist2;
//         foreach (var e in enumerable)
//         {
//             Console.WriteLine(e);
//         }

//         //使用 C# 中的 LINQ 方法將列表轉換為 IEnumerable
//         List<int> ilist3 = new List<int> { 1, 2, 3, 4, 5 };
//         IEnumerable<int> enumerable2 = ilist3.AsEnumerable();
//         foreach (var e in enumerable2)
//         {
//             Console.WriteLine(e);
//         }

//         ICollection b = new List<int>() { 10, 20, 30 };
//         foreach (int i in b)
//         {
//             Console.WriteLine(i);
//         }

//     }
// }