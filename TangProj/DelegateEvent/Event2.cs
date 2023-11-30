// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace DelegateEvent
// {
//     public class Event2
//     {
//         static void Main(string[] args)
//         {
//             Countdown countdown = new Countdown(3);
//             // 註冊事件
//             countdown.CountdownCompleted += Tip;
//             // 觸發事件
//             countdown.Decrement();
//             countdown.Decrement();
//             countdown.Decrement();
//         }

//         // 建立的方法，當事件發生時會呼叫
//         public static void Tip(object? sender, EventArgs eventArgs)
//         {
//             Console.WriteLine("Tip!!!");
//         }
//     }

//     public class Countdown
//     {
//         int internalCounter;

//         public event EventHandler? CountdownCompleted;

//         public Countdown(int n)
//         {
//             internalCounter = n;
//         }

//         protected virtual void OnCountdownCompleted(EventArgs e)
//         {
//             if (CountdownCompleted != null)
//                 CountdownCompleted(this, e); // 發送事件，同 CountdownCompleted.Invoke(this, e)
//         }

//         public void Decrement() // 倒數，直到 0 時呼叫事件
//         {
//             Console.WriteLine(internalCounter);
//             internalCounter = internalCounter - 1;
//             if (internalCounter == 0)
//                 OnCountdownCompleted(new EventArgs());
//         }
//     }
// }