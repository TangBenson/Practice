// using System;

// namespace CsharpStudy;
// class Passenger
// {
//     public void ReceiveNews(string news)
//     {
//         Console.WriteLine($"我收到一則新聞內容是:{news}");
//     }
// }
// class Gossiping
// {
//     public void Notify(Action<string> action, string news)
//     {
//         action(news);
//     }
// }
// class Delegate4
// {
//     static void Main()
//     {
//         Gossiping gossiping = new Gossiping();
//         Passenger passenger = new Passenger();
//         FourPercent fourPercent = new FourPercent();
//         Green green = new Green();

//         string news = "開放美豬進口了";
//         gossiping.Notify(passenger.ReceiveNews, news);
//         gossiping.Notify(fourPercent.Argue, news);
//         gossiping.Notify(green.Support, news);
//     }
// }

// class FourPercent
// {
//     public void Argue(string message)
//     {
//         Console.WriteLine($"政府要{message}，政府有夠爛");
//     }
// }

// class Green
// {
//     public void Support(string message)
//     {
//         Console.WriteLine($"Green:政府要{message}，FourPercent好了拉，又死不了");
//     }
// }