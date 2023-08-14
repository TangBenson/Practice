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
// // 對C# 而言，在做型別轉換時，撇開一些型別有提供Parse可供轉型外，通常我們有兩種選擇：一種是利用as運算子、一種則是強制轉型
// // C# 型別轉換盡量別用顯示(強制)轉型，在愈複雜的情境下，用as 會比較優，因為顯示轉型會出現Exception，造成程式錯誤，必須用try catch避免程式異常(效能耗費)
// // 顯式轉換int i=10; long l=(long)i; (顯式轉換包括所有的隱式轉換)
// // 隱式轉換int i=10; long l=i;
// // try catch 基本上一定會消耗記憶體資源，包覆愈多層，效能耗費就愈多 
// // as 也是將型別轉換型別，不成功會回傳null
// // is 是判斷型別是否為指定的型別類別
// // Boxing 是一種隱式轉型，將實值型別轉換成object型別。int a = 5; object b = (object)a;//boxing
// // Unboxing 是一種顯示轉型，將object轉換成其它型別。object c = 5; int d = (int)c;//unboxing 
// // 宣告變數時，型別帶"?"(問號)，表示變數可接受null值。C#中有分value type和reference type,而value type預設是不能有null    

// //定義貓的型別
// public class cat { }
// //定義狗的型別
// public class dog { }
// class TypeTang
// {
//     static void Main(string[] args)
//     {

//         object a = new cat();
//         dog b = new dog();

//         var temp = a as dog; // 對變數a 利用as 語法轉為 dog型別，結果temp變數得到 null 因為a的原始型別是cat
//         bool isKindDog = a is dog; // 結果:false
//         bool isKindCat = a is cat; // 結果:true
//                                    //var temp3 = (dog)a; // 會有嚴重的問題，將會出現Exception
//         Console.WriteLine(b.GetType()); //c_自我學習.dog
//         Console.WriteLine(a.GetType()); //c_自我學習.cat

//         int q1 = new int(); q1 = 5;
//         int q11 = 5;
//         Console.WriteLine(q1.GetType()); //System.Int32
//         Console.WriteLine(q11.GetType());//System.Int32
//                                          // string q2 = q1 as string; //因為q1是int，as用法規定要搭配reference type或可為null的類型，不可配value type

//         object o = new object();
//         string lb = o as string;
//         Console.WriteLine(lb);//不會印出null只會空白
//         if (lb == null) { Console.WriteLine("類型轉換失敗"); }
//         else { Console.WriteLine("類型轉換成功"); }

//     }
// }