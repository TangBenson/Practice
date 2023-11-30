// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// /*
// 在這個範例中，我們定義了一個 EventPublisher 類別來發布事件，它具有一個 MyEvent 事件。
// 我們也定義了一個 EventSubscriber 類別來訂閱事件，它在事件被觸發時會執行相應的處理方法。
// 在 Main 方法中，我們建立了一個 EventPublisher 物件和一個 EventSubscriber 物件。
// 然後，我們使用 TriggerEvent 方法來觸發事件並傳遞一個訊息。當事件被觸發時，訂閱者會收到事件並執行處理方法。
// 運行這個程式，你會在控制台上看到 "Received event: Hello, world!" 的輸出。
// 這只是一個簡單的 Event Driven 範例，你可以根據實際需求進行更複雜的事件處理和訂閱邏輯。
// */
// namespace DelegateEvent
// {
//     public class Event_Driven
//     {
//         public static void Main(string[] args)
//         {
//             EventPublisher publisher = new EventPublisher();
//             EventSubscriber subscriber = new EventSubscriber(publisher);

//             publisher.TriggerEvent("Hello, world!");

//             Console.ReadLine();
//         }
//     }

//     // 定義事件資料類別
//     public class MyEventArgs : EventArgs
//     {
//         public string? Message { get; set; }
//     }

//     // 定義事件發布者類別
//     public class EventPublisher
//     {
//         // 定義事件
//         public event EventHandler<MyEventArgs> MyEvent;

//         // 觸發事件
//         public void TriggerEvent(string message)
//         {
//             MyEventArgs eventArgs = new MyEventArgs { Message = message };
//             OnMyEvent(eventArgs);
//         }

//         // 發布事件
//         protected virtual void OnMyEvent(MyEventArgs e)
//         {
//             MyEvent?.Invoke(this, e);
//         }
//     }

//     // 定義事件訂閱者類別
//     public class EventSubscriber
//     {
//         public EventSubscriber(EventPublisher publisher)
//         {
//             // 訂閱事件
//             publisher.MyEvent += Publisher_MyEvent;
//         }

//         // 處理事件的方法
//         private void Publisher_MyEvent(object sender, MyEventArgs e)
//         {
//             Console.WriteLine("Received event: " + e.Message);
//         }
//     }
// }
