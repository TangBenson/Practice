// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace DelegateEvent;
// public class Event
// {
//     static void Main(string[] args)
//     {
//         Button button = new Button();

//         // Event Subscription事件訂閱 ~ Button_Click 方法訂閱了 Click 事件，事件訂閱是將事件監聽器注冊到事件源的過程
//         button.Click += Button_Click;
//         button.Click2 += Button_Click2;
//         button.Click3 += Button_Click3;

//         // 執行按鈕點擊操作
//         button.OnClick();
//         button.OnClick2();
//         button.OnClick3();
//     }

//     // Event Handler事件處理程序 ~ 也是 Event Listener事件监听器
//     static void Button_Click(object? sender, EventArgs e)
//     {
//         Console.WriteLine("Button click event handled!");
//     }

//     static void Button_Click2(object? sender, CustomEventArgs e)
//     {
//         // 在事件处理程序中访问传递的额外信息
//         Console.WriteLine($"Button2 click event handled! Message: {e.Message}");
//     }
//     static void Button_Click3(object? sender, string e)
//     {
//         // 在事件处理程序中访问传递的额外信息
//         Console.WriteLine($"Button3 click event handled! Message: {e}");
//     }
// }

// // Event Source事件源 ~ 包含定義事件和觸發事件
// public class Button
// {
//     // Event事件 ~ Click事件使用 EventHandler這個 delegate作為事件處理程序的類型，該 delegate類型也可自定義
//     public event EventHandler? Click;

//     public event EventHandler<CustomEventArgs>? Click2;
//     public event EventHandler<string>? Click3;

//     // 觸發事件的方法，用于觸發事件並調用事件處理程序
//     public void OnClick()
//     {
//         Console.WriteLine("OnClick");
//         // 检查事件是否有订阅者，并触发事件，EventArgs.Empty表示事件不須傳遞額外訊息
//         Click?.Invoke(this, EventArgs.Empty); // 寫法同 Click(this, EventArgs.Empty);
//     }

//     public void OnClick2()
//     {
//         Console.WriteLine("OnClick2");
//         // 传递自定义的事件参数
//         Click2?.Invoke(this, new CustomEventArgs("金子"));
//     }

//     public void OnClick3()
//     {
//         Console.WriteLine("OnClick3");
//         Click3?.Invoke(this, "銀子");
//     }
// }


// // 若要传递额外的信息给事件处理程序时，您可以自定义一个继承自 EventArgs 的事件参数类
// // 但我發現不繼承 EventArgs也可以，是否 .net6已經改掉了?
// public class CustomEventArgs //: EventArgs
// {
//     public string Message { get; }

//     public CustomEventArgs(string message)
//     {
//         Message = message;
//     }
// }