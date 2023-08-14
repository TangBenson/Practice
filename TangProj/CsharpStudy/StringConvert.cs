// using System;
// using System.Timers;
// using System.Collections.Generic; // 為了用IEnumerable

// namespace CsharpStudy;
// class StringConvert
// {
//     static void Main(string[] args)
//     {
//         // var a ="a123456789";
//         // int lenth = a.Length - 4 - 3;
//         // string replaceStr = a.Substring(4, 3);
//         // string specialStr = string.Empty;
//         // for (int i = 0; i < replaceStr.Length; i++)
//         // {
//         //     specialStr += '*';
//         // }
//         // a = a.Replace(replaceStr, specialStr);
//         // Console.Write(a);


//         // DateTime a = new DateTime();
//         // a = DateTime.Now;
//         // var s = a.ToString("yyyy-MM-dd");
//         // s = s.Replace("-",String.Empty);
//         // Console.WriteLine(s);
//         // var newid = s;
//         // string replaceStr = newid.Substring(0, 4);
//         // string specialStr = string.Empty;
//         // for (int i = 0; i < replaceStr.Length; i++)
//         // {
//         //     specialStr += '*';
//         // }
//         // s = newid.Replace(replaceStr, specialStr);
//         // Console.Write(s);


//         string[] empNameS =
//         {
//                 "龍龍一",
//                 "龍龍二",
//                 "龍龍三",
//                 "龍龍四",
//                 "龍龍五",
//                 "龍龍六",
//             };
//         string empName = "";
//         foreach (var x in empNameS)
//         {
//             empName += x.ToString() + ",";
//         }
//         empName = empName.TrimEnd(',');
//         Console.WriteLine(empName);
//         ShowValue<String>(empNameS);
//         Console.Read();
//     }

//     static void ShowValue<T>(IEnumerable<T> values)
//     {
//         Console.WriteLine("{0}", string.Join(",", values));
//     }
// }