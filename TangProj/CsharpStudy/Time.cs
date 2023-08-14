// using System;
// using System.Timers;

// namespace CsharpStudy;
// class Time
// {
//     //間隔2秒執行一次
//     // static void Main(string[] args)
//     // {
//     //     System.Timers.Timer timer = new System.Timers.Timer();
//     //     timer.Enabled = true;
//     //     timer.Interval = 2000; //執行間隔時間,單位為毫秒; 這裡實際間隔為10分鐘  
//     //     timer.Start();
//     //     timer.Elapsed += new System.Timers.ElapsedEventHandler(test); 
//     //     Console.ReadKey();
//     // }

//     // private static void test(object source, ElapsedEventArgs e)
//     // {
//     //       Console.WriteLine("OK, test event is fired at: " + DateTime.Now.ToString())        
//     // }

//     //指定每日10:30執行
//     //需要注意的是，由於是指定到特定分鐘執行事件，因此，timer.Inverval的時間間隔最長不得超過1分鐘，否則，長於1分鐘的時間間隔有可能會錯過10：30分這個時間節點，從而導致無法觸發該事件。
//     // static void Main(string[] args)
//     // {
//     //     System.Timers.Timer timer = new System.Timers.Timer();
//     //     timer.Enabled = true;
//     //     timer.Interval = 60000;//執行間隔時間,單位為毫秒;此時時間間隔為1分鐘  
//     //     timer.Start();
//     //     timer.Elapsed += new System.Timers.ElapsedEventHandler(test); 
//     //     Console.ReadKey();
//     // }

//     // private static void test(object source, ElapsedEventArgs e)
//     // {
//     //     if (DateTime.Now.Hour == 10 && DateTime.Now.Minute == 30)  //如果當前時間是10點30分
//     //         Console.WriteLine("OK, event fired at: " + DateTime.Now.ToString());
//     // }

//     //測試Timespan
//     // static void Main(string[] args)
//     // {
//     //     DateTime dt1 = new DateTime(2021, 11, 1);  
//     //     DateTime dt2 = new DateTime();  
//     //     dt2 = DateTime.Now;  
//     //     TimeSpan ts = new TimeSpan(dt2.Ticks - dt1.Ticks);  
//     //     //相差天數(未滿一天捨去,return int type)
//     //     Console.WriteLine(Convert.ToString( ts.Days ));
//     // }

//     // static void Main(string[] args)
//     // {
//     //     DateTime.Now.ToShortTimeString();
//     //     DateTime dt = DateTime.Now;
//     //     Console.WriteLine(DateTime.Now.ToString("yyyy")+DateTime.Now.ToString("MM")+DateTime.Now.ToString("dd")+DateTime.Now.ToString("HH")
//     //     +DateTime.Now.ToString("mm")+DateTime.Now.ToString("ss"));
//     // }


//     // static void Main(string[] args)
//     // {
//     //     string NOW = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//     //     Console.WriteLine(NOW);

//     //     DateTime start = new DateTime();
//     //     start = DateTime.Today;
//     //     Console.WriteLine(start);

//     //     string SD = (start == null) ? "" : Convert.ToDateTime(start).ToString("yyyy-MM-dd HH:mm:ss");
//     //     Console.WriteLine(SD);

//     //     DateTime sDate = DateTime.ParseExact(SD, "yyyy-MM-dd HH:mm:ss", null);      
//     //     Console.WriteLine(sDate);

//     //     string a = SD.Substring(0,10).Replace("-","");
//     //     int b = Int32.Parse(a);
//     //     Console.WriteLine(b);
//     //     Console.WriteLine(b.GetType());
//     // }

//     // static void Main(string[] args)
//     // {
//     //     Timer t = new Timer(699);
//     //     Console.WriteLine("{0}:{1}", t.Minutes, t.Seconds);
//     //     t.Seconds = 10;
//     //     Console.WriteLine("{0}:{1}", t.Minutes, t.Seconds);
//     //     t.Minutes = 19;
//     //     Console.WriteLine("{0}:{1}", t.Minutes, t.Seconds);
//     // }
// }

// class Timer
// {
//     private int _seconds = 0;
//     public Timer(int s)
//     {
//         _seconds = s;
//     }
//     public int Minutes
//     {
//         get { return _seconds / 60; }
//         set
//         {
//             _seconds += value * 60;
//         }
//     }
//     public int Seconds
//     {
//         get { return _seconds % 60; }
//         set
//         {
//             _seconds += value; // value是外部傳來的數字, 例如外部設t.Seconds = 10, value就是10
//         }
//     }
// }