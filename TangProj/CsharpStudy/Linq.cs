// using System;
// using System.Collections.Generic;
// using System.Text.RegularExpressions;
// // using System.Linq;

// namespace CsharpStudy;

// class Linq
// {
//     public class Student
//     {
//         public string? Name { set; get; }
//         public int Sexual { set; get; }
//         public int ID { set; get; }
//     }

//     public class BE_AaBb
//     {
//         public int A { get; set; }
//         public string? B { get; set; }
//     }

//     static void Main(string[] args)
//     {
//         #region 
//         // 範例一
//         int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//         var myLinq = from x in num select x; // 宣告式(Declarative)：(link)
//         var myLinq2 = num.Where(x => x > 5); // 編程式(Imperative)：(lambda)
//         foreach (var i in myLinq2)
//         {
//             Console.WriteLine(i);
//         }
//         Console.WriteLine("------範例一結束-------");

//         // 範例二
//         // List<Student> students = GetStudents();
//         var getstudents = GetStudents();
//         // var students = from st in getstudents select st;
//         // var students = from st in getstudents where st.Sexual==1 select st;
//         // var students = from st in getstudents orderby st.ID descending select st;
//         var students = getstudents.Where(x => x.Sexual == 1);
//         foreach (var i in students)
//         {
//             string sex = i.Sexual == 1 ? "male" : "female";
//             Console.WriteLine("Name:" + i.Name + ", Sexual:" + sex + ", ID:" + i.ID);
//         }
//         Console.WriteLine("------範例二結束-------");

//         // 範例三
//         // group by 群組 可以將資料依照群組分類，儲存成物件(包含 Key 及對應項目)
//         var getstudents2 = GetStudents();
//         // var students = from st in getstudents group st by st.Sexual ;
//         // var students2 = from st in getstudents2 where st.Name != "Tom" group st by st.Sexual into x where x.Key == 1 select x; // 使用into將group暫存在一個變數
//         var students2 = getstudents2.Where(x => x.Name != "Tom").GroupBy(x => x.Sexual).Where(x => x.Key == 1); // 意思同上
//         foreach (var i in students2)
//         {
//             //第一層迴圈取出group物件，可以透過 .Key得到group值
//             Console.WriteLine("Group物件的Key=" + i.Key);
//             foreach (var j in i)
//             {
//                 // 第二層迴圈可以取出Group對應資料
//                 Console.WriteLine(j.Name);
//             }
//         }
//         Console.WriteLine("------範例三結束-------");
//         #endregion

//         // 範例四
//         List<BE_AaBb> lstOut = new List<BE_AaBb>(){
//                 new BE_AaBb(){ A = 26, B = "Peggy" },
//                 new BE_AaBb(){ A = 29, B = "Zadvdfv" },
//                 new BE_AaBb(){ A = 29, B = "Anita" }
//             };
//         var L1 = lstOut.Where(x => x.B == "Peggy").ToList().Select(s => s.A);
//         List<string> PT1 = new List<string>() { };
//         var PT1str = String.Join(",", L1);
//         Console.WriteLine(L1.GetType());
//         Console.WriteLine(PT1str.GetType());

//         var L2 = lstOut.Select(s => s.B);
//         foreach (var i in L2)
//         {
//             Console.WriteLine(i);
//         }
//     }

//     public static List<Student> GetStudents()
//     {
//         List<Student> students = new List<Student>
//             {
//                 new Student {Name="Adam", Sexual=1, ID=1055010001},
//                 new Student {Name="Steven", Sexual=2, ID=1055010002},
//                 new Student {Name="Brown", Sexual=1, ID=1055010003},
//                 new Student {Name="Cindy", Sexual=2, ID=1055010004},
//                 new Student {Name="Tom", Sexual=1, ID=1055010005}
//             };
//         return students;
//     }
// }
