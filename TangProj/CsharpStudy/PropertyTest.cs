// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     class Girl
//     {
//         // 屬性（property）來提供對該類別的私有欄位（private field）的訪問和修改
//         private int _age = 27;
//         public string? name = "恩恩";
//         public int Age
//         {
//             get
//             {
//                 return _age;
//             }
//             set
//             {
//                 if (value < 0) { value = 0; }
//                 if (value > 0) { value = 60; }
//                 _age = value;
//             }
//         }
//         // iRentAPI都這樣宣告，但這屬性沒有修是任何欄位，不如直接宣告 public的欄位好了
//         public string? Like { set; get; }
//     }
//     class PropertyTest
//     {
//         static void Main(string[] args)
//         {
//             Girl peggy = new Girl();
//             Console.WriteLine($"{peggy.Age}--{peggy.name}--{peggy.Like}");
//             peggy.name = "山上優雅";
//             Console.WriteLine($"{peggy.Age}--{peggy.name}--{peggy.Like}");
//             peggy.Age = 30;
//             Console.WriteLine($"{peggy.Age}--{peggy.name}--{peggy.Like}");
//             peggy.Like="來一發";
//             Console.WriteLine($"{peggy.Age}--{peggy.name}--{peggy.Like}");
//         }
//     }
// }