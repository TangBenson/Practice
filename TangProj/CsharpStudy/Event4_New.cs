// using System;
// /*
// 委派是事件的基礎，就算使用.NET提供的EventHandler，但背後實作還是依靠委派delegate。
// 比較不同的地方在於，使用event關鍵字來限制委派的部分功能，讓委派不會被外部執行，但本質上還是委派，只是多了一道鎖。
// 另外事件的目的也只是單向傳遞，所以委派接受的方法並不會有回傳值，就像郵差派送出去也是送後不理，要回信要自己另外寄。
// 當然一方面的原因，也是因為委派本來就不能接收多個方法的回傳值，只能接受一個方法的回傳。
// */
// namespace CsharpStudy;
// class Event4_New
// {
//     static void Main(string[] args)
//     {
//         Subscriber 農夫 = new Subscriber() { subscriberName = "農夫" };
//         Newspaper 王國日報 = new Newspaper() { NewspaperName = "王國日報" };
//         Newspaper 帝國日報 = new Newspaper() { NewspaperName = "帝國日報" };
        
//         王國日報.newsEvent += 農夫.Notify;
//         帝國日報.newsEvent += 農夫.Notify;

//         Console.WriteLine("請輸入最新消息：");
//         string? news = Console.ReadLine();
//         王國日報.pushNews(news);

//         Console.WriteLine();
        
//         Console.WriteLine("請輸入最新消息：");
//         news = Console.ReadLine();
//         帝國日報.pushNews(news);
//     }
//     class Subscriber
//     {
//         public string? subscriberName;

//         public void Notify(object? sender, EventArgs eventArgs)
//         {
//             Newspaper? newspaper = sender as Newspaper; // 将 sender 对象转换为 Newspaper 类型
//             News? news = eventArgs as News; // 将 eventArgs 对象转换为 News 类型
//             Console.WriteLine($"我是{subscriberName}，我已經收到來自{newspaper!.NewspaperName}的{news!.title}：{news.content}");
//         }
//     }
//     class Newspaper
//     {
//         public string? NewspaperName;

//         public event EventHandler? newsEvent;

//         public void pushNews(string? msg)
//         {
//             News news = new News() { title = "最新快訊", content = msg };
//             OnNewsEvent(news);
//         }

//         protected void OnNewsEvent(News news)
//         {
//             newsEvent?.Invoke(this, news);
//         }
//     }

//     /*
//     .NET類別庫本身提供了現成的已經定義好的委派 => public delegate void EventHandler(object sender, EventArgs eventArgs);
//     sender就是來源物件，型別是最廣泛的object，而傳遞參數集合宣告的型別是.NET類別庫定義好的EventArgs
//     要使用定義好的委派EventHandler，就要將我們定義好的類別繼承自EventArgs
//     */
//     class News : EventArgs
//     {
//         public string? title;
//         public string? content;
//     }
// }