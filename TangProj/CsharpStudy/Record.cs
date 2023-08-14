// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// /*
// 從 C# 9 開始，需要自訂型別的時候，除了類別（class）、結構（struct），
// 現在多了一個選擇：記錄（record）。其主要用途是封裝資料，特別是不可改變的資料（immutable data）。
// 記錄可以說是一種特殊的類別。它跟類別一樣可以宣告成抽象記錄（abstract record），也可以繼承另一個記錄。
// 預設情況下，程式碼經過編譯之後，記錄都是以類別的形式存在。換言之，這些記錄都是參考型別（reference type），而非實質型別（value type)。

// 從 C# 10 開始，則可以明確宣告一個底層為結構（struct）的記錄。例如：
//     public record struct Student(...);
// 以此方式定義的記錄類型便是實質型別，也就不允許繼承了。

// OS：其宣告方式有點不同，若要像宣告class那樣也是可以，但實務上通常不建議這樣寫，因為 record 的主要用途是為了封裝不可變的資料，而註解的範例卻提供了可隨時修改的屬性。
// */
// namespace CsharpStudy;
// class Record
// {
//     //「位置參數」的寫法來宣告record
//     public record Student(int Id, string Name);

//     //自訂 record 類型
//     public record Student2(int Id, string Name)
//     {
//         // C# 9 中新支援了 init 關鍵字，這是一個特殊的 setter，用來指定只能在物件初始化的時候進行賦值
//         // init 類似於 set ，但是 init 對應的欄位會是一個 readonly 的欄位，來保證只能在構造器中或者初始化器中被賦值
//         protected int Id { get; init; } 
//         public string Name { get; } = Name; //改成唯讀屬性
//         // 以上兩行等於告訴編譯器：「我要自行定義這兩個屬性的行為。」
//         public int Grade { get; init; }
//     }

//     static void Main(string[] args)
//     {
//         Student stu1 = new(1, "Mike");
//         Student stu2 = new(1, "Mike") { Id = 2 };
//         Console.WriteLine(stu1.Id);
//         Console.WriteLine(stu2.Id);

//         // var stu = new StudentQ { Id = 1, Name = "Mike" };
//         // stu.Id = 10;
//     }
// }

// // public record StudentQ
// // {
// //     public int Id { get; set; }
// //     public string Name { get; set; }
// // }
