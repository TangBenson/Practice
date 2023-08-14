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
// //教你讀進json並把內容取語出
// class Readjson
// {
//     static void Main(string[] args)
//     {
//         static string ExtractJsonText(string raw)
//         {
//             // 字會黏在一起
//             // var node = JsonConvert.DeserializeXmlNode(raw, "Root");
//             // return node.InnerText;

//             // 讓字分開
//             var json = raw.TrimStart(' ', '\t', '\r', '\n');
//             // if (json.StartsWith("["))
//             //     json = $@"{{ ""Array"": {json} }}";
//             var node = JsonConvert.DeserializeXmlNode(json, "Root");
//             return //將Element Tag換成空白，再去除連續空白
//                 string.Join(" ",
//                     Regex.Replace(node.InnerXml, "<[^>]+>", " ")
//                         .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
//                         .ToArray());
//         }
//         // var json = System.IO.File.ReadAllText("C:\\Users\\benson922\\Desktop\\Org2.json");
//         var json = System.IO.File.ReadAllText("Org2.json");
//         string[] s = ExtractJsonText(json).Split(" ");
//         for (int i = 0; i < 3; i++)
//         {
//             Console.WriteLine(s[i]);
//         };
//         // Console.WriteLine(ExtractJsonText(json));
//         // Console.Read();
//     }
// }