// using System;
// using System.Collections.Generic;
// using System.Text;

// namespace DelegateEvent;
// class Delegate
// {
//     delegate void MyFunction(string text); //宣告委派型別，注意!這是一個型別喔
//     static event MyFunction? readKeyEvent; //宣告公開委派型別變數。型別帶"?"(問號)，表示變數可接受null值
//     //加上event後，C#編譯器就會禁止外部來執行這個委派
//     //加上event後，委派型別變數才能被當成介面(Interface)定義的一部分，這是一般的欄位(Field)變數無法辦到的
//     static void Main(string[] args)
//     {
//         // readKeyEvent = new MyFunction(myFunction1);
//         readKeyEvent += myFunction1; // 寫法同上

//         Console.WriteLine("請輸入一字串");
//         string text = Console.ReadLine()!; // +!表示變數不可能為null
//         Console.WriteLine();

//         // 第一次觸發事件
//         readKeyEvent.Invoke(text); //等同：readKeyEvent(text);
//         Console.WriteLine();

//         readKeyEvent += new MyFunction(myFunction2);//事件綁定第2個方法

//         Console.WriteLine("請輸入一字串");
//         text = Console.ReadLine()!;
//         Console.WriteLine();

//         // 第二次觸發事件
//         readKeyEvent.Invoke(text);
//         Console.WriteLine();

//         Console.WriteLine("按任意鍵離開");
//         Console.ReadKey();
//     }

//     static void myFunction1(string text)
//     {
//         Console.WriteLine("這是 myFunction1 函數");
//         Console.WriteLine("參數=" + text);
//     }

//     static void myFunction2(string text)
//     {
//         Console.WriteLine("這是 myFunction2 函數");
//         Console.WriteLine("參數=" + text);
//     }
// }