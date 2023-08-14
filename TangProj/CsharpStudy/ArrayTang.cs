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

// class ArrayTang
// {
//     static void Main(string[] args)
//     {
//         #region []陣列：特定型別，固定長度的陣列，長度需事先宣告
//         // //一維
//         // int[] n = new int[5] { 50, 60, 70, 80, 90 };
//         // int[] k = new int[] { 501, 601, 701, 801, 901 };
//         // int[] o = { 51, 61, 71, 81, 91 };
//         // Console.WriteLine($"n:{n[0]}");
//         // int[] q = new int[5];
//         // int value = 7;
//         // for (int r = 0; r < 5; r++)
//         // {
//         //     q[r] = value++;
//         // }
//         // Console.WriteLine($"q:{q[0]}");
//         // int[] id1 = { 44, 26, 92, 30, 71, 38 };
//         // int[] id2 = { 39, 59, 83, 47, 26, 4, 30 };
//         // IEnumerable<int> both = id1.Intersect(id2);
//         // Console.WriteLine("交集: {0}", string.Join(",", both));
//         // //二、三維很少用
//         // int[,] s = new int[,]{{7,8},{9,5}};
//         // int[,] s2 = new int[2,2]{{70,80},{90,50}};
//         // Console.WriteLine($"s:{s2[1,1]}");
//         // int[,,] p = { 
//         //     { 
//         //         { 1, 2, 3 }, 
//         //         { 4, 5, 6 } 
//         //     }, 
//         //     { 
//         //         { 7, 8, 9 }, 
//         //         { 10, 11, 12 } 
//         //     } 
//         // };
//         // Console.WriteLine(p[1, 1, 1]);
//         // //一維 (物件型別)
//         // int apiLen = 3;
//         // object[] objparms = new object[apiLen == 0 ? 1 : apiLen];
//         // Console.WriteLine($"顯示:{objparms.Length}");
//         // for (int i = 0; i < apiLen; i++)
//         // {
//         //     //new後面不用加型別嗎? 是object才這樣?
//         //     objparms[i] = new
//         //     {
//         //         cat = "米克斯",
//         //         dog = "哈士奇"
//         //     };
//         // }
//         // //賦值後...奇怪這不是二維嗎?
//         // Console.WriteLine($"哈卍:{objparms[0]}");
//         // Console.WriteLine($"哈兔:{objparms[1]}");
//         // //二、三維很少用
//         // object[,] o2 = { { 3, 4 }, { 5, 6 } };
//         // Console.WriteLine($"顯示o2:{o2[1,1]}");
//         // //這好像不是二維，這是啥?
//         // //這樣宣告的話，裡面一定要放object?
//         // object[][] parms1 = {
//         //         new object[] {
//         //             'p',
//         //             'o',
//         //             'i',
//         //             'u',
//         //             'y'
//         //         },
//         //         objparms
//         //     };
//         // object[][] o3 = { 
//         //     new object[] { 7, 8 }, 
//         //     new object[] { 9 } 
//         // };
//         // Console.WriteLine($"parms1[0][0]:{parms1[0][0]}");
//         // Console.WriteLine($"parms1[0][1]:{parms1[0][1]}");
//         // Console.WriteLine($"parms1[1][0]:{parms1[1][0]}");
//         // Console.WriteLine($"o3[1][0]:{o3[1][0]}");
//         // Console.WriteLine(parms1.Length);
//         // // 這個就不懂，但元素一定要是object，[][][]的元素要是[][]，[][]的元素要是[]
//         // object[][][] o5 = new object[][][] { new object[][] { new object[] { 9 }, new object[] { 9 } } };
//         // Console.WriteLine($"{o5.Length} ~ {o5}");
//         #endregion



//         #region ArrayList：不特定型別，不固定長度的陣列，靠儲存Object(弱型別)來達成非特定型態儲存，所以在執行階段調用時，會有轉型的成本跟風險，盡量避免使用。
//         // ArrayList arrayList1 = new ArrayList();
//         // //新增資料
//         // arrayList1.Add("cde");
//         // arrayList1.Add(5678);
//         // arrayList1.Add(9999);
//         // //修改資料
//         // arrayList1[2] = 34;
//         // //移除資料
//         // arrayList1.RemoveAt(0);
//         // //插入資料
//         // arrayList1.Insert(0, "qwe");
//         #endregion



//         #region List：特定型別，不固定長度的陣列
//         // C# 裡 List用法，有點像是不用宣告長度的陣列(Array)
//         List<int> myList = new List<int>();
//         myList.Add(770);
//         myList.Add(880);
//         myList.Add(1314520);
//         myList.Add(999);
//         myList[2] = 777;
//         int last = myList.Last(); // LINQ 中的 Last() 函式獲取資料結構的最後一個元素
//         Console.WriteLine(last);
//         int last2 = myList.LastOrDefault();
//         Console.WriteLine(last2);
//         var lastItem = myList[^1];
//         Console.WriteLine(lastItem);
//         myList.RemoveAt(0);
//         // foreach就是python的for阿，我怎麼現在才看出來....蠢耶
//         foreach (int i in myList)
//         {
//             Console.WriteLine(i);
//         }
//         List<MyModule> tt = new List<MyModule>(){
//                 new MyModule(){FF="角色扮演",RR="爽感賽車"},
//                 new MyModule(){FF="RPG",RR="Race"}
//             };
//         Console.WriteLine(tt[0]);
//         List<string> list1 = new List<string> { "1", "2", "3", "4", "5" };
//         List<string> list2 = new List<string> { "3", "4", "5", "6", "8" };
//         List<string> list3 = new List<string>();
//         list3 = list1.Intersect(list2).ToList();
//         #endregion



//         #region Dictionary
//         // 不建議
//         Dictionary<string, string> MyDic = new Dictionary<string, string>();
//         MyDic.Add("Name", "Jack");
//         MyDic.Add("Blog", "Jack’s Blog");
//         MyDic.Add("Group", "KTV Group");
//         Console.WriteLine(MyDic["Name"]);
//         // 建議
//         Dictionary<string, string> dctNewWay = new Dictionary<string, string>()
//             {
//                 {"Key1", "AAAA"}, {"Key2", "BBBB"},
//                 {"Key3", "CCCC"}, {"Key4", "DDDD"}
//             };
//         Console.WriteLine(dctNewWay["Key3"]);
//         #endregion


//     }
// }

// class MyModule
// {
//     public string? FF { set; get; }
//     public string? RR { set; get; }
// }

