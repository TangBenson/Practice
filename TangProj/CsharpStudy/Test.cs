// using System;
// using System.Text;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;

// namespace CsharpStudy;
// class Test
// {
//     static void Main(string[] args)
//     {
//         // Console.WriteLine("天空一聲巨響，老娘閃亮登場");

//         // //字串以逗號分隔成List
//         // string a = "a,b,c,d";
//         // List<string> list = new List<string>(a.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
//         // foreach(string i in list){
//         //     Console.WriteLine(i);
//         // }

//         // DateTime a = DateTime.Now;
//         // Console.WriteLine(a);
//         // DateTime v = DateTime.Now.AddHours(3);
//         // Console.Write(v);

//         // int year = 2019;
//         // DateTime date_1 = new DateTime(year, 01, 01);
//         // DateTime date_2 = new DateTime(year, 02, 02);
//         // int diffDay = ((date_2 - date_1).Days) + 1;
//         // Console.Write(diffDay);



//         // string a = "[{\"MenuCode\":\"A\",\"SubMenuCode\":\"A01\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":0},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A02\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":1},{\"Code\":\"Query\",\"hasPower\":1},{\"Code\":\"Add\",\"hasPower\":1},{\"Code\":\"Edit\",\"hasPower\":1}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A030\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":0},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A04\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":1},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A07\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":1},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A05\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":0},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}]";

//         // string b = "[{\"MenuCode\":\"A\",\"SubMenuCode\":\"A01\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":0},{\"Code\":\"Query\",\"hasPower\":1},{\"Code\":\"Add\",\"hasPower\":1},{\"Code\":\"Edit\",\"hasPower\":1}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A02\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":1},{\"Code\":\"Query\",\"hasPower\":1},{\"Code\":\"Add\",\"hasPower\":1},{\"Code\":\"Edit\",\"hasPower\":1}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A031\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":0},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A04\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":1},{\"Code\":\"Query\",\"hasPower\":1},{\"Code\":\"Add\",\"hasPower\":1},{\"Code\":\"Edit\",\"hasPower\":1}]}," +
//         //             "{\"MenuCode\":\"A\",\"SubMenuCode\":\"A06\",\"PowerList\":[{\"Code\":\"View\",\"hasPower\":0},{\"Code\":\"Query\",\"hasPower\":0},{\"Code\":\"Add\",\"hasPower\":0},{\"Code\":\"Edit\",\"hasPower\":0}]}]";

//         // List<Power> p1 = JsonConvert.DeserializeObject<List<Power>>(a);
//         // List<Power> p2 = JsonConvert.DeserializeObject<List<Power>>(b);

//         // var lst1 = p1.Select(x => x.SubMenuCode).Intersect(p2.Select(x => x.SubMenuCode));//交集
//         // var lst2 = p1.Select(x => x.SubMenuCode).Union(p2.Select(x => x.SubMenuCode));//聯集
//         // var lst3 = p1.Select(x => x.SubMenuCode).Except(p2.Select(x => x.SubMenuCode));//差集 1才有
//         // var lst4 = p2.Select(x => x.SubMenuCode).Except(p1.Select(x => x.SubMenuCode));//差集 2才有

//         // // List<Power> p3 = new List<Power>(){
//         // //     new Power(){MenuCode="角色扮演",SubMenuCode="爽感賽車",PowerList=null}
//         // // };
//         // List<Power> p3 = new List<Power>();

//         // foreach (string i in lst3)
//         // {
//         //     Power obj1 = new Power();
//         //     obj1.MenuCode = p1.Where(x => x.SubMenuCode == i).Select(x => x.MenuCode).First();
//         //     obj1.SubMenuCode = p1.Where(x => x.SubMenuCode == i).Select(x => x.SubMenuCode).First();
//         //     obj1.PowerList = p1.Where(x => x.SubMenuCode == i).Select(x => x.PowerList).First();
//         //     p3.Add(obj1);
//         // };
//         // foreach (string i in lst4)
//         // {
//         //     Power obj1 = new Power();
//         //     obj1.MenuCode = p2.Where(x => x.SubMenuCode == i).Select(x => x.MenuCode).First();
//         //     obj1.SubMenuCode = p2.Where(x => x.SubMenuCode == i).Select(x => x.SubMenuCode).First();
//         //     obj1.PowerList = p2.Where(x => x.SubMenuCode == i).Select(x => x.PowerList).First();
//         //     p3.Add(obj1);
//         // };
//         // foreach (string i in lst1)
//         // {
//         //     int v1 = p1.Where(x => x.SubMenuCode == i).Select(x => x.PowerList[0].hasPower).First(); //群組的匯出權限
//         //     int v2 = p2.Where(x => x.SubMenuCode == i).Select(x => x.PowerList[0].hasPower).First(); //個人的匯出權限
//         //     int q1 = p1.Where(x => x.SubMenuCode == i).Select(x => x.PowerList[1].hasPower).First(); //群組的觀看權限
//         //     int q2 = p2.Where(x => x.SubMenuCode == i).Select(x => x.PowerList[1].hasPower).First(); //個人的觀看權限

//         //     Power obj1 = new Power();
//         //     obj1.MenuCode = p1.Where(x => x.SubMenuCode == i).Select(x => x.MenuCode).First();
//         //     obj1.SubMenuCode = p1.Where(x => x.SubMenuCode == i).Select(x => x.SubMenuCode).First();
//         //     obj1.PowerList = p1.Where(x => x.SubMenuCode == i).Select(x => x.PowerList).First(); //先塞入群組權限，後面再改
//         //     if (v2 == 1 && v1 == 0)
//         //     {
//         //         obj1.PowerList[0].hasPower = 1;
//         //     }
//         //     if (q2 == 1 && q1 == 0)
//         //     {
//         //         obj1.PowerList[1].hasPower = 1;
//         //         obj1.PowerList[2].hasPower = 1;
//         //         obj1.PowerList[3].hasPower = 1;
//         //     }
//         //     p3.Add(obj1);
//         // }
//         // foreach (Power i in p3)
//         // {
//         //     Console.WriteLine(i.SubMenuCode);
//         // }


//         // var data = new SyncOutput_PushMessageResultParam()
//         // {
//         //     Result = false,
//         //     Data = null//0,
//         // };
//         // string a = "{\"Result\":true,\"Message\":\"\",\"Data\":{\"BatchNo\":1509811,\"MessageIds\":[12211050]}}";
//         // data = System.Text.Json.JsonSerializer.Deserialize<SyncOutput_PushMessageResultParam>(a);
//         // Console.WriteLine($"Responce data:{data.Result}");


//         // var d = DateTime.Now;
//         // Console.WriteLine(d);
//         // Console.WriteLine(d.ToString("yyyyMMdd"));


//         // Console.WriteLine(Encoding.Default);

//         // string ee = "jjjj";
//         // string id = string.IsNullOrEmpty(ee) ? "qqq" : ee;
//         // Console.WriteLine(ee);
//         // Console.WriteLine(id);

//         string jsonStr = @"
//         {
//             ""schema"": {
//                 ""type"": ""struct"",
//                 ""optional"": false
//             },
//             ""payload"": {
//                 ""before"": [
//                     {
//                         ""authSeq"": 1,
//                         ""CPType"": ""CP0101"",
//                         ""CarNo"": ""RCG-2261"",
//                         ""CarType"": ""01"",
//                         ""GiveMin"": 10,
//                         ""UseFlag"": 1
//                     }
//                 ],
//                 ""after"": [
//                     {
//                         ""authSeq"": 1,
//                         ""CPType"": ""CP0101"",
//                         ""CarNo"": ""RCG-2261"",
//                         ""CarType"": ""01"",
//                         ""GiveMin"": 20,
//                         ""UseFlag"": 0
//                     }
//                 ],
//                 ""source"": {
//                     ""version"": ""2.3.0.Alpha1"",
//                     ""connector"": ""sqlserver""
//                 },
//                 ""op"": ""u""
//             }
//         }";
//         DiscountLabelHistoryLog? envelope = System.Text.Json.JsonSerializer.Deserialize<DiscountLabelHistoryLog>(jsonStr);

//         List<Content>? beforeData = envelope.payload.before;
//         List<Content>? afterData = envelope.payload.after;
//         Dictionary<string, object> sourceData = envelope.payload.source;
//         string opData = envelope.payload.op;

//         // 輸出所有欄位值
//         foreach (var kvp in beforeData)
//         {
//             Console.WriteLine($"{kvp.GiveMin}");
//         }
//         Console.WriteLine(afterData[0].GiveMin);
//         foreach (var kvp in sourceData)
//         {
//             Console.WriteLine($"{kvp.Key}:{kvp.Value}");
//         }
//         Console.WriteLine(opData);
//     }
// }
// // public class SyncOutput_PushMessageResultParam
// // {
// //     public bool Result { get; set; }
// //     public string Message { get; set; }
// //     public DataType Data { get; set; } //若呼叫半殘型api要改成long type
// // }
// // public class DataType
// // {
// //     public int BatchNo { get; set; }
// //     public int[] MessageIds { get; set; }
// // }
// // public class Power
// // {
// //     public string MenuCode { get; set; } = "";
// //     public string SubMenuCode { get; set; } = "";
// //     public List<PowerList> PowerList { get; set; }
// // }
// // public class PowerList
// // {
// //     public string Code { get; set; } = "";
// //     public int hasPower { get; set; }
// // }


// public class DiscountLabelHistoryLog
// {
//     // public Schema schema { get; set; }
//     public Payload? payload { get; set; }
// }

// public class Payload
// {
//     public List<Content>? before { get; set; }
//     public List<Content>? after { get; set; }
//     public Dictionary<string, object>? source { get; set; }
//     public string? op { get; set; }
// }
// public class Content
// {
//     public long? authSeq { get; set; }
//     public string? CPType { get; set; }
//     public string? CarNo { get; set; }
//     public string? CarType { get; set; }
//     public short? GiveMin { get; set; }
//     public byte? UseFlag { get; set; }
// }