// using System;
// using System.Collections.Generic; // 為了使用List
// using Newtonsoft.Json; // 為了使用JsonConvert
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using System.Text.Encodings.Web;
// using Newtonsoft.Json.Linq;

// namespace CsharpStudy;
// //序列化 & 反序列化
// class Json
// {
//     public class Student
//     {
//         public int ID { get; set; }
//         public string Name { get; set; } = "";
//         public int Age { get; set; }
//         public string Sex { get; set; } = "";
//     }
//     static void Main(string[] args)
//     {
//         var student = new Student
//         {
//             ID = 0,
//             Name = "飛翼0式",
//             Age = 5,
//             Sex = "鋼彈"
//         };
//         List<Student> lstStuModel = new List<Student>(){
//             new Student(){ID=1,Name="張飛",Age=250,Sex="男"},
//             new Student(){ID=2,Name="潘金蓮",Age=300,Sex="女"}
//         };
//         List<int> lstStuModel2 = new List<int>(){
//             55,66,77
//         };
//         List<Student> lstHighStuModel = new List<Student>();
//         lstHighStuModel.Add(new Student() { ID = 1, Name = "微軟", Age = 50, Sex = "男" });
//         lstHighStuModel.Add(new Student() { ID = 2, Name = "谷哥", Age = 20, Sex = "女" });
//         object[][] obj = {
//             new object[2] { "AA","BB" },
//             new object[2] { "CC","DD" }
//         };
//         object[] obj2 = {
//             "C","D",99
//         };
//         Dictionary<string, string> dctNewWay = new Dictionary<string, string>()
//         {
//             {"第一關", "AAAA"}, {"第二關", "BBBB"},
//             {"第三關", "CCCC"}, {"第四關", "DDDD"}
//         };

//         // //Newtonsoft.Json序列化
//         // Console.WriteLine("--------------------序列化Serialize-------------------");
//         // string jsonData0 = JsonConvert.SerializeObject(student);
//         // Console.WriteLine(jsonData0);
//         // string jsonData1 = JsonConvert.SerializeObject(lstStuModel);
//         // Console.WriteLine(jsonData1);
//         // string jsonData2 = JsonConvert.SerializeObject(lstStuModel2);
//         // Console.WriteLine(jsonData2);
//         // string jsonData3 = JsonConvert.SerializeObject(obj);
//         // Console.WriteLine(jsonData3);
//         // string jsonData4 = JsonConvert.SerializeObject(obj2);
//         // Console.WriteLine(jsonData4);
//         // string jsonData5 = JsonConvert.SerializeObject(dctNewWay);
//         // Console.WriteLine(jsonData5);

//         // //Newtonsoft.Json反序列化
//         // Console.WriteLine("--------------------反序列化Deserialize-------------------");
//         // string json = @"{ 'Name':'C#','Age':'3000','ID':'1','Sex':'女'}"; //加上@就是告訴編譯器,接下來的這個字串我會全權負責,請編譯器不要自做聰明,只要如實照做就好了
//         // Student a = new Student();
//         // Student descJsonStu = _ = JsonConvert.DeserializeObject<Student>(json) ?? a;//反序列化，_捨棄和??聯合運算子是c#7新語法，判斷null
//         // Console.WriteLine(string.Format("反~~~~序列化： ID={0},Name={1},Age={2},Sex={3}", descJsonStu.ID, descJsonStu.Name, descJsonStu.Age, descJsonStu.Sex));
//         // Console.WriteLine(JsonConvert.DeserializeObject<Student>(jsonData0).GetType().Name);//Student
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData0).GetType().Name);//JObject
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData0));//JObject
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData1));//JArray
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData2));//JArray
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData3));//JArray
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData4));//JArray
//         // Console.WriteLine(JsonConvert.DeserializeObject(jsonData5));//JObject

//         // //System.Text.Json序列化
//         // Console.WriteLine("--------------------序列化Serialize----------------------");
//         // var options = new JsonSerializerOptions { 
//         //     Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping //正確輸出中文和符號
//         //     // ,WriteIndented = true //輸出 JSON 時內容會省略多餘的換行和空白
//         //     // ,PropertyNameCaseInsensitive = true // 不區分大小寫
//         //     // ,AllowTrailingCommas = true // 忽略結尾逗號
//         //     // ,ReadCommentHandling = JsonCommentHandling.Skip // 忽略註解
//         //     // ,MaxDepth = 100 // System.Text.Json 在 JSON 的最大深度預設為 64 (在 ASP.NET Core 中預設為 32)
//         // };
//         // var sjsonData0 = System.Text.Json.JsonSerializer.Serialize<Student>(student, options); //多帶options參數讓中文可正常顯示
//         // Console.WriteLine(sjsonData0); //是string型別
//         // string sjsonData = System.Text.Json.JsonSerializer.Serialize(lstStuModel, options);
//         // Console.WriteLine(sjsonData);
//         // string sjsonData2 = System.Text.Json.JsonSerializer.Serialize(lstStuModel2);
//         // Console.WriteLine(sjsonData2);
//         // string sjsonData3 = System.Text.Json.JsonSerializer.Serialize(obj);
//         // Console.WriteLine(sjsonData3);
//         // string sjsonData4 = System.Text.Json.JsonSerializer.Serialize(obj2);
//         // Console.WriteLine(sjsonData4);
//         // string sjsonData5 = System.Text.Json.JsonSerializer.Serialize(dctNewWay, options);
//         // Console.WriteLine(sjsonData5);

//         // //System.Text.Json反序列化
//         // Console.WriteLine("--------------------反序列化Deserialize-------------------");
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<Student>(sjsonData0));//Student
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<List<Student>>(sjsonData));//List`1
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<List<int>>(sjsonData2));//List`1
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<object>(sjsonData3));//JsonElement
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<object>(sjsonData4));//JsonElement
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(sjsonData5));//Dictionary`2

//         // //System.Text.Json讀取Json檔並序列化
//         // Console.WriteLine("----------------------------------------------------------");
//         // var json2 = File.ReadAllText(@"test.json");
//         // var jsonEx = System.Text.Json.JsonSerializer.Deserialize<object>(json2);
//         // Console.WriteLine(jsonEx);//JsonElement
//         // var jd = JsonDocument.Parse(json2);
//         // Console.WriteLine(jd);//JsonDocument
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(jd, options));
//         // var root = jd.RootElement;
//         // Console.WriteLine(root.GetProperty("臨").GetString());
//         // Console.WriteLine(root.GetProperty("住"));
//         // Console.WriteLine(root.GetProperty("住").GetType());//JsonElement
//         // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(root.GetProperty("住"), options));

//         //Newtonsoft.Json的 JObject
//         // string jStr = JsonConvert.SerializeObject(lstHighStuModel); //這種序列化的字串格式和下一行不同，不能用在JObject.Parse
//         string jStr = "{\"name\": \"steam\",\"games\":[{\"name\":\"The Witcher 3\",\"price\":\"63\"},{\"name\":\"GTA5\",\"price\":\"93\"}],\"master\":\"G pan\"}";
//         JObject jSon = JObject.Parse(jStr);
//         foreach (var item in jSon)
//         {
//             Console.WriteLine(item.Key + "," + item.Value);
//         }
//         Console.WriteLine(JsonConvert.DeserializeObject(jStr));
//     }
// }