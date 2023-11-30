// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// /*
// 黑大：依之前的理解，Class上宣告事件的目的在提供呼叫端能於特定時機觸發自訂邏輯，標準做法是先定義委派型別(Delegate)，
// 再宣告一個公開委派型別變數並加上event關鍵字，就算是為類別定義出了事件，之後呼叫端便能用+=, -=加上或移除事件邏輯，實現"事件"的運作概念。

// 不過以下範例宣告委派型別後我們定義了兩個公開變數，不管有沒有加註event關鍵字，一樣都可用+=掛上多個函數，
// 同樣都能用if (someDelegate != null) someDelegate();的寫法觸發，看起來加不加event關鍵字並不影響其運作效果，那麼，加上event關鍵字的意義到底是什麼?

// 事實上，宣告event與否，並不會讓編譯出的程式本體有所不同(該文作者動用IL反組譯證明了這點)，但使用event關鍵字會產生以下四點差異:

// 1. 只有透過加註event，該委派型別變數才能被當成介面(Interface)定義的一部分，這是一般的欄位(Field)變數無法辦到的，此點可算event關鍵字最主要的功能。
// 2. 另外還有一點很大差異，委派型別變數加上event後，就只能透過類別內部的程式碼呼叫，不允許外部直接執行之。以上述程式為例，執行boo.DeletateTest();是OK的，
//    但boo.EventTest();光在編譯時就過不了關，會出現以下錯誤: The event 'ConsoleApplication1.Program.Boo.EventTest' can only appear ...
// 3. 相較於屬性(Property)有get; set;，加上event後，我們可以實作add; remove;，可在呼叫端加掛或移除事件時執行自訂邏輯。
// 4. 在.NET Framework裡，所有宣告event關鍵字的事件，應遵循someEvent(object source, EventArgs e)的參數慣例，但自行開發時的事件，編譯器或IDE倒不會強制檢查就是了。
// */
// namespace DelegateEvent
// {
//     public class Event3
//     {
//         public class Boo
//         {
//             public delegate void MyDelegate(int i);
//             //兩個delegate，其中一個加註event關鍵字
//             public MyDelegate? DelegateTest;
//             public event MyDelegate? EventTest;
//             //在Test()中觸發兩個delegate
//             public void Test()
//             {
//                 int i = DateTime.Now.Second;
//                 if (DelegateTest != null) 
//                     DelegateTest(i); // 寫法同 DelegateTest.Invoke(i)，Invoke可省略
//                 if (EventTest != null) 
//                     EventTest(i);
//             }
//         }
//         static void Main(string[] args)
//         {
//             Boo boo = new Boo();
            
//             //兩種做法都可用+=方式掛上多個事件
//             boo.DelegateTest += (i) =>
//             {
//                 Console.WriteLine("Delegate A: {0}", i);
//             };
//             boo.DelegateTest += (i) =>
//             {
//                 Console.WriteLine("Delegate B: {0}", i);
//             };
//             boo.EventTest += (i) =>
//             {
//                 Console.WriteLine("Event A: {0}", i);
//             };
//             boo.EventTest += (i) =>
//             {
//                 Console.WriteLine("Event B: {0}", i);
//             };
//             boo.Test();
//         }
//     }
// }