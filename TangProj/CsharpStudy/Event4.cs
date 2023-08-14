// using System;
// /*
// https://ithelp.ithome.com.tw/articles/10228852

// 委託的定義和方法的定義類似，只是在前面加了一個delegate,但委託不是方法，它是一種類型。
// 是一種特殊的類型,看成是一種新的對象類型比較好理解。用於對與該委託有相同簽名的方法調用。
// */
// namespace CsharpStudy;
// class Event4
// {
//     //訂閱者
//     class Subscriber
//     {
//         public string? name;

//         //*Event Handler事件處理程序 ~ 也是 Event Listener事件监听器
//         public void Notice(string info)
//         {
//             Console.WriteLine($"我是{name}，我已經收到最新新聞：{info}");
//         }
//     }

//     //*發行者，Event Source事件源 
//     class Newspaper
//     {
//         public delegate void NoticeMember(string 新聞報導);

//         // public NoticeMember? news; //宣告委派型別作為事件來使用
//         public event NoticeMember? news; //加上event後，C#編譯器就會禁止外部來執行這個委派

//         //撰寫一個新聞投稿方法，當有人投稿了新聞的那一刻，就要觸發事件去通知訂閱新聞的人
//         public void OnContribution(string newsfile)
//         {
//             //觸發事件
//             news!(newsfile);
//         }
//     }

//     //以上實作了發行者與訂閱者，並且用委派實作的事件驅動程式。主程式，包含訂閱新聞與投稿新聞來觸發事件
//     static void Main(string[] args)
//     {
//         Subscriber 農夫 = new Subscriber() { name = "拓荒者" };
//         Subscriber 商人 = new Subscriber() { name = "煉金師" };
//         Subscriber 騎士 = new Subscriber() { name = "大劍豪" };
//         Subscriber 法師 = new Subscriber() { name = "咒術師" };
//         Newspaper 王國日報 = new Newspaper();

//         //*訂閱，Event Subscription事件訂閱
//         王國日報.news += 農夫.Notice; //Notice要傳入一個參數，但這邊沒傳，而是Invoke才做
//         王國日報.news += 商人.Notice;
//         王國日報.news += 騎士.Notice;
//         王國日報.news += 法師.Notice;

//         // 王國日報.news.Invoke("fake news"); //禁止外部來執行這個委派

//         //退出訂閱
//         // 王國日報.最新新聞 -= 農夫.通知我;
//         // 王國日報.最新新聞 -= 騎士.通知我;

//         while (true)
//         {
//             Console.WriteLine("請輸入最新消息：");
//             string? newsinfo = Console.ReadLine();
//             王國日報.OnContribution(newsinfo!);
//         }
//     }
// }